import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";
import { AuthReducer } from "./AuthReducer";
import { FilterReducer } from "./FilterReducer";
import { PagingReducer } from "./PagingReducer";

import { ComponentTypesReducer } from "./ComponentTypesReducer";
import { ComponentsReducer } from "./ComponentsReducer";
import { SelectedComponentReducer } from "./SelectedComponentReducer";
import { SelectedNavigationComponentReducer } from "./SelectedNavigationComponentReducer";
import { ShoppingCartReducer } from "./ShoppingCartReducer";


const reducers = combineReducers({
  brandsState: BrandsReducer,
  userState: AuthReducer,
  componentTypesState: ComponentTypesReducer,
  filterState: FilterReducer,
  pagingState: PagingReducer,
  componentsState: ComponentsReducer,
  selectedComponentState: SelectedComponentReducer,
  selectedNavigationComponent: SelectedNavigationComponentReducer,
  shoppingCartState: ShoppingCartReducer
});

export default reducers;
