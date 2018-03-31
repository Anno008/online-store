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

export const fetchComponents = (filter, paging) => dispatch => {
  dispatch(fetchingComponents());
  const config = {
    method: "GET",
    url: `${apiUrl}/components?componentName=${filter.componentName}&
        BrandId=${filter.brandId}&
        componentTypeId=${filter.componentTypeId}
        page=${paging.page}&
        pageSize=${paging.pageSize}&
        orderBy=${0}`
  };
  return apiCall(config)
    .then(result => dispatch(fetchingComponentsSucceeded(result)))
    .catch(error => dispatch(fetchingComponentsFailed(error.message)));
};
