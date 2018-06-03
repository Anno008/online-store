import actions from "../actions/Actions";

export const ChatRoomReducer = (state, action) => {
  switch (action.type) {
    case actions.FETCHING_CHAT_ROOM_MESSAGES:
      return { ...state, isFetching: true };
    case actions.FETCHING_CHAT_ROOM_MESSAGES_SUCCESS:
      return { ...state, data: action.data, error: false, isFetching: false };
    case actions.FETCHING_CHAT_ROOM_MESSAGES_FAILURE:
      return { ...state, data: [], error: action.error, isFetching: false };
    default:
      return { ...state };
  }
};