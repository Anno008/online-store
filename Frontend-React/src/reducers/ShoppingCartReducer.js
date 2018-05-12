import actions from "../actions/Actions";

export const ShoppingCartReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_SHOPPING_CART:
      return { ...state, isFetching: true };
    case actions.FETCHING_SHOPPING_CART_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_SHOPPING_CART_FAILURE:
      return { ...state, error: action.error, isFetching: false };
    default:
      return { ...state };
  }
};