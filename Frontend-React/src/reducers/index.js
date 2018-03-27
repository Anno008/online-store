import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";
import { AuthReducer } from "./AuthReducer";
import { ComponentTypesReducer } from "./ComponentTypesReducer";

const reducers = combineReducers({
   brandsState: BrandsReducer,
   userState: AuthReducer,
   componentTypesState: ComponentTypesReducer
});

export default reducers;
