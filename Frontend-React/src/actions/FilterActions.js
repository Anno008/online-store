import actions from "./Actions";

export const searchBarTextChanged = text => dispatch =>
  dispatch({ type: actions.SEARCH_BAR_TEXT_CHANGED, data: text });

export const brandSelectionChanged = brandId => dispatch =>
  dispatch({ type: actions.BRAND_SELECTION_CHANGED, data: brandId });

export const componentTypeSelectionChanged = componentTypeId => dispatch =>
  dispatch({
    type: actions.COMPONENT_TYPE_SELECTION_CHANGED,
    data: componentTypeId
  });
