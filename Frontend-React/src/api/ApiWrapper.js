import { apiUrl, redirectUri } from "../constants";
import jwt_decode from "jwt-decode";

const apiCall = async config => {
  const request = {
    data: config.body,
    headers: config.needsAuth ? await createHeader() : {},
    method: config.method || "GET",
    url: config.url
  };

  return await fetch(request.url, {
    method: request.method,
    body: request.method == "GET" ? undefined : JSON.stringify(request.data),
    headers: request.headers
  })
    .then(response => response.json().then(content => content))
    .catch(error => {
      throw new Error(error);
    });
};

const createHeader = async () => {
  const headers = new Headers();
  headers.append("Content-Type", "application/json");
  let token = localStorage.getItem("accessToken");

  if (token) {
    const decoded = jwt_decode(token);

    // Access token has expired
    if (decoded.exp * 1000 < Date.now()) {
      const refreshToken = localStorage.getItem("refreshToken");
      if (refreshToken) {
        const refreshedAccessToken = await fetch(`${apiUrl}/Auth/auth`, {
          method: "POST",
          headers: headers,
          body: JSON.stringify({ grantType: "refresh_token" })
        })
          .then(response => response.json().then(res => res.accessToken))
          .catch(error => {
            throw new Error("Failed to refresh access token");
          });
        token = refreshedAccessToken;
      } else {
        location.href = redirectUri;
      }
    }
  } else {
    location.href = redirectUri;
  }

  if (token) {
    headers.append("authorization", `bearer ${token}`);
  }

  return headers;
};

export default apiCall;
