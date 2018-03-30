import actions from "../actions/Actions";

const initialComponentFilter = { componentName: "", brandId: 0, componentTypeId: 0 };

export const FilterReducer = (state = initialComponentFilter, action) => {
  switch (action.type) {
    case actions.SEARCH_BAR_TEXT_CHANGED:
      return { ...state, componentName: action.data };
    case actions.BRAND_SELECTION_CHANGED:
      return { ...state, componentName: action.data };
    case actions.COMPONENT_TYPE_SELECTION_CHANGED:
      return { ...state, componentName: action.data };
    default:
      return { ...state };
  }
};
