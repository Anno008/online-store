import actions from "../actions/Actions";

export const BrandsReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_BRANDS:
      return { ...state, data: [], isFetching: true };
    case actions.FETCHING_BRANDS_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_BRANDS_FAILURE:
      return { ...state, data: [], error: true, isFetching: false };
    default:
      return { ...state };
  }
};