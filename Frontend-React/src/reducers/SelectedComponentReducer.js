import actions from "../actions/Actions";

export const SelectedComponentReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_ONE_COMPONENT:
      return { ...state, isFetching: true };
    case actions.FETCHING_ONE_COMPONENT_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_ONE_COMPONENT_FAILURE:
      return { ...state, error: action.error, isFetching: false };
    case actions.RESET_SELECTED_COMPONENT:
      return { ...state, data: undefined};
    default:
      return { ...state };
  }
};
