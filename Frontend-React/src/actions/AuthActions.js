import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl, clientId } from "../constants";
import jwt_decode from "jwt-decode";

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
      location.href = "/";
      return dispatch(
        authSuccessful(decodedToken.user_name, decodedToken.roles)
      );
    })
    .catch(error => {
      return dispatch(authFailed(error.message));
    });
};

export const checkForUser = () => dispatch => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    const decodedToken = jwt_decode(token);
    return dispatch(authSuccessful(decodedToken.user_name, decodedToken.roles));
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
      dispatch(authFailed(error.message));
    });
};

export const logout = () => dispatch => {
  localStorage.clear();
  dispatch(logoutUser());
};
