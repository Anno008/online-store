import actions from "./Actions";
import { apiUrl } from "../constants";
import { apiCall } from "../api/ApiWrapper";

const getData = () => ({ type: actions.FETCHING_BRANDS });

const getDataSuccess = data => ({
  type: actions.FETCHING_BRANDS_SUCCESS,
  data
});

const getDataFailure = error => ({
  type: actions.FETCHING_BRANDS_FAILURE,
  error
});

export const fetchBrands = () => dispatch => {
  dispatch(getData());

  const config = {
    method: "GET",
    needsAuth: false,
    url: `${apiUrl}/brands`
  };

  return apiCall(config.url, config.needsAuth, config.method)
    .then(result => dispatch(getDataSuccess(result)))
    .catch(error => dispatch(getDataFailure(error.message)));
};
