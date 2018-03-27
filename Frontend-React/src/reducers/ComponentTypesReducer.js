import actions from "../actions/Actions";

export const ComponentTypesReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_COMPONENT_TYPES:
      return { ...state, data: [], isFetching: true };
    case actions.FETCHING_COMPONENT_TYPES_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_COMPONENT_TYPES_FAILURE:
      return { ...state, data: [], error: action.error, isFetching: false };
    default:
      return { ...state };
  }
};
