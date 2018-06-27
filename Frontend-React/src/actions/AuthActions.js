import actions from "./Actions";
import { apiUrl, clientId } from "../constants";
import jwt_decode from "jwt-decode";
import { navigationComponentChanged } from "./SelectedNavigationComponentActions";
import { keys } from "../navigation/NavigationKeys";
import { apiCall } from "../api/ApiWrapper";

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

  let data =  {
    username: username,
    password: password,
    clientId: clientId,
    rememberMe: rememberMe,
    grantType: "password"
  };

  return apiCall(`${apiUrl}/auth/auth`, false, "POST", data)
      .then(result => {
        localStorage.setItem("accessToken", result.accessToken);
        if(result.refreshToken) {
          localStorage.setItem("refreshToken", result.refreshToken);
        }
        const decodedToken = jwt_decode(result.accessToken);
        dispatch(navigationComponentChanged(keys.catalog));
        return dispatch(authSuccessful(decodedToken.username, decodedToken.roles));
      })
      .catch(error => dispatch(authFailed(error.message)));
};

export const checkForUser = () => dispatch => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    const decodedToken = jwt_decode(token);
    dispatch(navigationComponentChanged(keys.catalog));
    return dispatch(authSuccessful(decodedToken.username, decodedToken.roles));
  } else {
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

  return apiCall(config.url, config.needsAuth, config.method, config.data)
    .then(result => {
        localStorage.setItem("accessToken", result.accessToken);
        if(result.refreshToken) {
          localStorage.setItem("refreshToken", result.refreshToken);
        }
        const decodedToken = jwt_decode(result.accessToken);
        dispatch(navigationComponentChanged(keys.catalog));
        return dispatch(authSuccessful(decodedToken.username, decodedToken.roles));
    })
    .catch(error => dispatch(authFailed(error.message)));
};

export const logout = () => dispatch => {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
  return dispatch(logoutUser());
};
