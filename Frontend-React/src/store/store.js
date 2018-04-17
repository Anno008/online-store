import { applyMiddleware, createStore } from "redux";
import thunk from "redux-thunk";
import reducers from "reducers";
import { debounce } from "lodash";
import { loadState, saveState } from "./StateLoader";

const initialState = loadState();

const store = createStore(reducers, initialState, applyMiddleware(thunk));

store.subscribe(() => debounce(() => saveState(store.getState()), 10000)());

export default () => store;
