import actions from "./Actions";

export const navigationComponentChanged = key => dispatch =>
  dispatch({ type: actions.SELECTED_NAVIGATION_COMPONENT_CHANGED, data: key });