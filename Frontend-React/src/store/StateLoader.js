export const loadState = () =>
  JSON.parse(localStorage.getItem("ReactStoreState")) || {};

export const saveState = state =>
  localStorage.setItem("ReactStoreState", JSON.stringify(state));
