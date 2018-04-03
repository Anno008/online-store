import actions from "./Actions";

export const pageNumberChanged = page => dispatch =>
  dispatch({ type: actions.PAGE_CHANGED, data: page });

export const pageSizeChanged = pageSize => dispatch =>
  dispatch({ type: actions.PAGE_SIZE_CHANGED, data: pageSize });
