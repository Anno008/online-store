import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl } from "../constants";
import { navigationComponentChanged } from "./SelectedNavigationComponentActions";

const fetchingShoppingCart = () => ({ type: actions.FETCHING_SHOPPING_CART });

const fetchingShoppingCartSucceeded = data => ({
  type: actions.FETCHING_SHOPPING_CART_SUCCESS,
  data
});

const fetchingShoppingCartFailed = error => ({
  type: actions.FETCHING_SHOPPING_CART_FAILURE,
  error
});

export const fetchShoppingCart = () => dispatch => {
  dispatch(fetchingShoppingCart());
  const config = {
    method: "GET",
    needsAuth: true,
    url: `${apiUrl}/shoppingCarts`
  };

  return apiCall(config)
    .then(result => dispatch(fetchingShoppingCartSucceeded(result)))
    .catch(error => dispatch(fetchingShoppingCartFailed(error.message)));
};

export const addComponentToShoppingCart = componentId => dispatch => {
  const config = {
    method: "POST",
    needsAuth: true,
    url: `${apiUrl}/shoppingcarts`,
    data: componentId
  };

  return apiCall(config);
};

export const removeComponentFromShoppingCart = componentId => dispatch => {
  const config = {
    method: "DELETE",
    needsAuth: true,
    url: `${apiUrl}/shoppingCarts`,
    data: componentId
  };

  return apiCall(config).then(result => dispatch(fetchShoppingCart()));
};
