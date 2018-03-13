import { combineReducers } from "redux";

const temp = (state = {}, action = {}) => {
    return { ...state };
};

const reducers = combineReducers({
   temp
});

export default reducers;

