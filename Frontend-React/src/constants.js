export const apiUrl = process.env.NODE_ENV == "production" ? "https://backon.herokuapp.com/api" : "http://localhost:8080/api";
export const apiSocketUrl = process.env.NODE_ENV == "production" ? "ws://backon.herokuapp.com" : "ws://localhost:8080";
export const clientId = "react-frontend";
export const reactStoreState = "reactStoreState";
