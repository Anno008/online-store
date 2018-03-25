import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl } from "../constants";

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
    needsAuth: true,
    url: `${apiUrl}/componentTypes`
  };

  return apiCall(config)
    .then(result => dispatch(getDataSuccess(result)))
    .catch(error => dispatch(getDataFailure(error.message)));
};
