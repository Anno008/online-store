import actions from "../actions/Actions";
import { keys } from "../navigation/NavigationKeys";

const initialComponent = { component: "catalog"};

export const SelectedNavigationComponentReducer = (state , action) => {
  switch (action.type) {
    case actions.SELECTED_NAVIGATION_COMPONENT_CHANGED:
      return { data: action.data };
    default:
      return { data: keys.catalog };
  }
};
