import actions from "./Actions";
import { apiUrl } from "../constants";
import { logout, checkForUser } from "./AuthActions";
import { navigationComponentChanged } from "./SelectedNavigationComponentActions";
import { keys } from "../navigation/NavigationKeys";
import { apiCall } from "../api/ApiWrapper";

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

  return apiCall(`${apiUrl}/shoppingCarts`, true, "GET")
    .then(result => dispatch(fetchingShoppingCartSucceeded(result)))
    .catch(err => {
      dispatch(navigationComponentChanged(keys.catalog));
      dispatch(checkForUser());
    });
};

export const addComponentToShoppingCart = componentId => dispatch => {
  const config = {
    method: "POST",
    needsAuth: true,
    url: `${apiUrl}/shoppingcarts`,
    data: componentId
  };
  return apiCall(config.url, config.needsAuth, config.method, config.data)
    .catch(error => dispatch(logout()))
};

export const removeComponentFromShoppingCart = componentId => dispatch => {
  const config = {
    method: "DELETE",
    needsAuth: true,
    url: `${apiUrl}/shoppingCarts`,
    data: componentId
  };

  return apiCall(config.url, config.needsAuth, config.method)
    .then(_ => dispatch(fetchShoppingCart()))
    .catch(error => dispatch(logout()));
};
