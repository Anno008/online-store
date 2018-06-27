import actions from "./Actions";
import { apiUrl } from "../constants";
import { apiCall } from "../api/ApiWrapper";

const getData = () => ({ type: actions.FETCHING_COMPONENT_TYPES });

const getDataSuccess = data => ({
  type: actions.FETCHING_COMPONENT_TYPES_SUCCESS,
  data
});

const getDataFailure = error => ({
  type: actions.FETCHING_COMPONENT_TYPES_FAILURE,
  error
});

export const fetchComponentTypes = () => dispatch => {
  dispatch(getData());

  const config = {
    method: "GET",
    needsAuth: false,
    url: `${apiUrl}/componentTypes`
  };

  return apiCall(config.url, config.needsAuth, config.method)
    .then(result => dispatch(getDataSuccess(result)))
    .catch(error => dispatch(getDataFailure(error.message)));
};
