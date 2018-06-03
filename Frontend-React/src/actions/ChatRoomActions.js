import actions from "./Actions";
import apiCall from "../api/ApiWrapper";
import { apiUrl } from "../constants";

const getData = () => ({ type: actions.FETCHING_BRANDS });

const getDataSuccess = data => ({
  type: actions.FETCHING_CHAT_ROOM_MESSAGES_SUCCESS,
  data
});

const getDataFailure = error => ({
  type: actions.FETCHING_CHAT_ROOM_MESSAGES_FAILURE,
  error
});

export const fetchChatRoomMessages = () => dispatch => {
  dispatch(getData());
  const config = {
    method: "GET",
    needsAuth: true,
    url: `${apiUrl}/ChatRoomMessages`
  };

  return apiCall(config)
    .then(result => dispatch(getDataSuccess(result)))
    .catch(error => dispatch(getDataFailure(error.message)));
};
