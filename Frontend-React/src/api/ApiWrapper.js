import { apiUrl, clientId } from "../constants";
import jwt_decode from "jwt-decode";

export const apiCall = async (url, needsToken = false, method = "GET", data) => 
  await makeRestCall(url, method, data, await createHeaders(needsToken));

const createHeaders = async (needsToken) => {
  const headers = new Headers();
  headers.append("Content-Type", "application/json");

  if (needsToken) {
    headers.append("authorization", `bearer ${await acquireAccessToken()}`);
  }

  return headers;
};

const acquireAccessToken = async () => {
  let existingToken = localStorage.getItem("accessToken");
  let token;

  if (existingToken) {
    const decoded = jwt_decode(existingToken);
    if (decoded.exp * 1000 < Date.now()) {
        localStorage.removeItem("accessToken");
        const refreshToken = localStorage.getItem("refreshToken");
      if (refreshToken) {
          token = await refreshAccessToken(refreshToken)
            .then(response => response);
      }
    } else {
    token = existingToken;
    }
  }

  if(!token){
      throw new Error("Token not defined");
  }
  
  localStorage.setItem("accessToken", token);
  return token;
};

const refreshAccessToken = async (refreshToken) =>
    await makeRestCall
    (
      `${apiUrl}/Auth/auth`,
      "POST",
      { grantType: "refresh_token", refreshToken: refreshToken, clientId: clientId }, 
      await createHeaders(false)
    );

const makeRestCall = async (url, method = "GET", data, headers) => 
  await fetch(url,
  {
    method,
    body: method == "GET" ? undefined : JSON.stringify(data),
    headers
  })
    .then(response => {
      if (response.status >= 200 && response.status < 300) {
        return response.json().then(content => content);
      } else {
        return response.text().then(text => {
          throw text;
        });
      }
    })
    .catch(error => {
      throw new Error(error);
    });