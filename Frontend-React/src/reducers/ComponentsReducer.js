import actions from "../actions/Actions";

export const ComponentsReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_COMPONENTS:
      return { ...state, data: [], isFetching: true };
    case actions.FETCHING_COMPONENTS_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_COMPONENTS_FAILURE:
      return { ...state, data: [], error: action.error, isFetching: false };
    default:
      return { ...state };
  }
};
