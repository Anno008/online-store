import actions from "../actions/Actions";

export const AuthReducer = (state, action) => {
  switch (action.type) {
    case actions.AUTH_IN_PROGRESS:
      return { ...state, data: {}, isFetching: true };
    case actions.AUTH_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.AUTH_FAILURE:
      return { ...state, data: {}, error: action.error, isFetching: false };
    default:
      return { ...state };
  }
};
