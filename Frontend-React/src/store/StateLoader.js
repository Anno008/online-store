export const loadState = () => {
  try {
    let serializedState = localStorage.getItem("ReactStoreState");
    if (serializedState === null) {
      return {};
    }

    return JSON.parse(serializedState);
  } catch (err) {
    return {};
  }
};

export const saveState = state => {
  try {
    let serializedState = JSON.stringify(state);
    localStorage.setItem("ReactStoreState", serializedState);
  } catch (err) {}
};
