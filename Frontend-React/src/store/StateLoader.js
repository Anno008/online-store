import { reactStoreState } from "../constants";

export const loadState = () =>
  JSON.parse(localStorage.getItem(reactStoreState)) || {};

export const saveState = state =>
  localStorage.setItem(reactStoreState, JSON.stringify(state));
