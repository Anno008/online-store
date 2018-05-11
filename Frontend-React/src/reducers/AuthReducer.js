import actions from "../actions/Actions";

export const AuthReducer = (state, action) => {
  switch (action.type) {
    case actions.AUTH_IN_PROGRESS:
      return { ...state, isFetching: true };
    case actions.AUTH_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.AUTH_FAILURE:
      return { ...state, error: action.error, isFetching: false };
    case actions.LOGOUT_USER:
      return { error: false, isFetching: false };
    default:
      return { ...state };
  }
};
