import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl } from "../constants";

const searchBarTextChanged = text => dispatch =>
  dispatch({ type: actions.SEARCH_BAR_TEXT_CHANGED, data: text });

const brandSelectionChanged = brandId => dispatch =>
  dispatch({ type: actions.BRAND_SELECTION_CHANGED, data: brandId });

const componentTypeSelectionChanged = componentTypeId => dispatch =>
  dispatch({
    type: actions.COMPONENT_TYPE_SELECTION_CHANGED,
    data: componentTypeId
  });
