import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";
import { AuthReducer } from "./AuthReducer";
import { FilterReducer } from "./FilterReducer";
import { PagingReducer } from "./PagingReducer";

import { ComponentTypesReducer } from "./ComponentTypesReducer";
import { ComponentsReducer } from "./ComponentsReducer";

const reducers = combineReducers({
  brandsState: BrandsReducer,
  userState: AuthReducer,
  componentTypesState: ComponentTypesReducer,
  filterState: FilterReducer,
  pagingState: PagingReducer,
  componentsState: ComponentsReducer
});

export default reducers;
