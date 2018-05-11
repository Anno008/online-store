import actions from "../actions/Actions";
import { keys } from "../navigation/NavigationKeys";

const initialState = { data: keys.catalog };

export const SelectedNavigationComponentReducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.SELECTED_NAVIGATION_COMPONENT_CHANGED:
      return { data: action.data };
    default:
      return { ...state };
  }
};
