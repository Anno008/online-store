import { combineReducers } from "redux";
import { BrandsReducer } from "./BrandsReducer";

const reducers = combineReducers({
   brandsState: BrandsReducer
});

export default reducers;
