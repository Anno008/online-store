import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";
import { AuthReducer } from "./AuthReducer";

const reducers = combineReducers({
   brandsState: BrandsReducer,
   userState: AuthReducer
});

export default reducers;
