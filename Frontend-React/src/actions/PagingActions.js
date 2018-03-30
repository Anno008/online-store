import actions from "./Actions";

const pageNumberChanged = page => dispatch =>
  dispatch({ type: actions.PAGE_CHANGED, data: page });

const pageSizeChanged = pageSize => dispatch =>
  dispatch({ type: actions.PAGE_SIZE_CHANGED, data: pageSize });
