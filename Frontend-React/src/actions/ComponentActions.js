import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl } from "../constants";

const fetchingComponents = () => ({ type: actions.FETCHING_COMPONENTS });

const fetchingComponentsSucceeded = data => ({
  type: actions.FETCHING_COMPONENTS_SUCCESS,
  data
});

const fetchingComponentsFailed = error => ({
  type: actions.FETCHING_COMPONENTS_FAILURE,
  error
});

const fetchingOneComponent = () => ({ type: actions.FETCHING_ONE_COMPONENT });

const fetchingOneComponentSucceeded = data => ({
  type: actions.FETCHING_ONE_COMPONENT_SUCCESS,
  data
});

const fetchingOneComponentFailed = error => ({
  type: actions.FETCHING_ONE_COMPONENT_FAILURE,
  error
});

export const fetchComponents = (filter, paging) => dispatch => {
  dispatch(fetchingComponents());

  const query =
    `${apiUrl}/components?componentName=${filter.componentName}&` +
    `brandId=${filter.brandId}&` +
    `componentTypeId=${filter.componentTypeId}&` +
    `page=${paging.page}&` +
    `pageSize=${paging.pageSize}`;

  const config = {
    method: "GET",
    url: query
  };

  return apiCall(config)
    .then(result => dispatch(fetchingComponentsSucceeded(result)))
    .catch(error => dispatch(fetchingComponentsFailed(error.message)));
};

export const componentSelected = id => dispatch => {
  dispatch(fetchingOneComponent());
  
  const config = {
    method: "GET",
    url: `${apiUrl}/components/${id}`
  };

  return apiCall(config)
    .then(result => {
      dispatch(fetchingOneComponentSucceeded(result));
      console.log(result);
    }
    )
    .catch(error => dispatch(fetchingOneComponentFailed(error.message)));
}
