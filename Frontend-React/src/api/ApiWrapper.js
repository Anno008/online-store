import { apiUrl, redirectUri } from "../constants";
import jwt_decode from "jwt-decode";

const apiCall = async config => {
  console.log(config);
  console.log(apiUrl);

  const request = {
    data: config.body,
    headers: config.needsAuth ? await createHeader(config.headers)
      : config.headers,
    method: config.method || "GET",
    url: config.url
  };

  return await fetch(request.url, {
    method: request.method,
    body: request.method == "GET" ? undefined : request.data,
    headers: request.headers
  })
    .then(response => {
      response;
      console.log(response);
    })
    .catch(error => {
      console.log(error);
      throw new Error(error);
    });
};

const createHeader = async () => {
  let token = localStorage.getItem("accessToken");
  if (token) {
    const decoded = jwt_decode(token);
    if (decoded.exp * 1000 < Date.now()) {
    //   token = await getRefreshToken(localStorage.getItem(refreshToken));
    }
  } else {
    location.href = redirectUri;
    return;
  }

  return token
    ? { authorization: `bearer ${token}`, ...(headers || {}) }
    : headers || {};
};

export default apiCall;
