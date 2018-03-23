import { apiUrl, redirectUri } from "../constants";
import jwt_decode from "jwt-decode";

const apiCall = async config => {
  const request = {
    data: config.data,
    headers: await createHeader(config.needsAuth ? true : false),
    method: config.method || "GET",
    url: config.url
  };

  return await fetch(request.url, {
    method: request.method,
    body: request.method == "GET" ? undefined : JSON.stringify(request.data),
    headers: request.headers
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
};

const createHeader = async needsToken => {
  const headers = new Headers();
  headers.append("Content-Type", "application/json");

  if (needsToken) {
    headers.append("authorization", `bearer ${await acquireAccessToken()}`);
  }

  return headers;
};

const acquireAccessToken = async () => {
  let token = localStorage.getItem("accessToken");
  if (token) {
    const decoded = jwt_decode(token);
    // Access token has expired
    if (decoded.exp * 1000 < Date.now()) {
      const refreshToken = localStorage.getItem("refreshToken");
      if (refreshToken) {
        return await fetch(`${apiUrl}/Auth/auth`, {
          method: "POST",
          headers: headers,
          body: JSON.stringify({ grantType: "refresh_token" })
        })
          .then(response => response.json().then(res => res.accessToken))
          .catch(error => {
            throw new Error("Failed to refresh access token");
          });
      } else {
        location.href = redirectUri;
      }
    }
  }
};

export default apiCall;
