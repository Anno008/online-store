import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";
import { AuthReducer } from "./AuthReducer";

const reducers = combineReducers({
   brandsState: BrandsReducer,
   authState: AuthReducer
});

export default reducers;
