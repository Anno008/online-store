import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl, clientId } from "../constants";
import jwt_decode from "jwt-decode";
import { navigationComponentChanged } from "./SelectedNavigationComponentActions";
import { keys } from "../navigation/NavigationKeys";

const authInProgress = () => ({ type: actions.AUTH_IN_PROGRESS });

const authSuccessful = (username, roles) => ({
  type: actions.AUTH_SUCCESS,
  data: { username, roles }
});

const authFailed = error => ({
  type: actions.AUTH_FAILURE,
  error: error
});

const logoutUser = () => ({ type: actions.LOGOUT_USER });

export const login = (username, password, rememberMe) => dispatch => {
  dispatch(authInProgress());
  const config = {
    method: "POST",
    data: {
      username: username,
      password: password,
      clientId: clientId,
      rememberMe: rememberMe,
      grantType: "password"
    },
    url: `${apiUrl}/auth/auth`
  };

  return apiCall(config)
    .then(result => {
      localStorage.setItem("accessToken", result.accessToken);
      localStorage.setItem("refreshToken", result.refreshToken);

      const decodedToken = jwt_decode(result.accessToken);
      return dispatch(
        authSuccessful(decodedToken.sub, decodedToken.roles)
      );
    })
    .catch(error => {
      // dispatch(navigationComponentChanged(keys.auth));
      return dispatch(authFailed(error.message));
    });
};

export const checkForUser = () => dispatch => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    const decodedToken = jwt_decode(token);
    return dispatch(authSuccessful(decodedToken.sub, decodedToken.roles));
  } else {
    // Clearing local storage just in case
    return dispatch(logout());
  }
};

export const register = (username, password) => dispatch => {
  dispatch(authInProgress());

  const config = {
    method: "POST",
    needsAuth: false,
    data: {
      username: username,
      password: password,
      clientId: clientId
    },
    url: `${apiUrl}/auth/register`
  };

  return apiCall(config)
    .then(token => {
      localStorage.setItem("accessToken", token.accessToken);
      const decodedToken = jwt_decode(token.accessToken);
      return dispatch(
        authSuccessful(decodedToken.user_name, decodedToken.roles)
      );
    })
    .catch(error => {
      // navigationComponentChanged(keys.auth);
      dispatch(authFailed(error.message));
    });
};

export const logout = () => dispatch => {
  localStorage.removeItem("accessToken");
  return dispatch(logoutUser());
};
