import actions from "../actions/Actions";

const initialPaging = { page: 1, pageSize: 10 };

export const PagingReducer = (state = initialComponentFilter, action) => {
  switch (action.type) {
    case actions.PAGE_CHANGED:
      return { ...state, page: action.data };
    case actions.PAGE_SIZE_CHANGED:
      return { ...state, pageSize: action.data };
    default:
      return { ...state };
  }
};
