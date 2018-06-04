import React from "react";
import { connect } from "react-redux";
import { apiSocketUrl } from "../../constants";
import "components/css/chatRoom.css";
import { fetchChatRoomMessages } from "actions/ChatRoomActions";
import { LoaderComponent } from "../Utils/LoaderComponent";

class ChatRoomComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      socket: {},
      message: "",
      allMessages: [],
      intervalId: 0
    };

    this.sendMessage = this.sendMessage.bind(this);
    this.enterKeyHandler = this.enterKeyHandler.bind(this);
    this.transformMessageData = this.transformMessageData.bind(this);
  }

  componentDidMount() {
    // Adding the interval to state so it can be cleaned up on dismounting
    this.setState({ intervalId: this.initializeWebSocket() });
  }

  componentWillUnmount() {
    // Clearing the interval on component dismounting
    clearInterval(this.state.intervalId);
  }

  initializeWebSocket() {
    let start = () => {
      // re initializing the web socket
      var ws = new WebSocket(apiSocketUrl);
      this.setState({
        socket: ws
      });

      // fetching all previous messages
      ws.onopen = () => {
        this.props.getAllChatRoomMessages();
      };

      // appending the message to the message collection
      ws.onmessage = e => {
        let response = this.transformMessageData(JSON.parse(e.data));
        this.setState(prevState => ({
          allMessages: [...prevState.allMessages, response]
        }));
      };
    };

    // If the socket is any other state then open try to connect again
    let check = () => {
      if (!this.state.socket || this.state.socket.readyState !== 1) start();
    };

    return setInterval(check, 5000);
  }

  transformMessageData(data) {
    let response = data;
    let date = new Date(response.date);
    let formatedDate = `${date.getUTCFullYear()}/${date.getUTCMonth()}/${date.getUTCDate()}    
                        ${date.getHours()}:${date.getUTCMinutes()}:${date.getUTCSeconds()}`;
    response.date = formatedDate;
    return response;
  }

  componentWillReceiveProps(props) {
    if (props.chatRoomState.data !== undefined) {
      this.setState(prevState => ({
        allMessages: props.chatRoomState.data.map(x =>
          this.transformMessageData(x)
        )
      }));
    }
  }

  componentDidUpdate() {
    this.messagesDiv.scrollTop = this.messagesDiv.scrollHeight;
  }

  enterKeyHandler(e) {
    if (e.key === "Enter") {
      this.sendMessage();
    }
  }

  sendMessage(e) {
    var msg = {
      message: this.state.message,
      user: this.props.userState.data.username,
      date: Date.now()
    };

    console.log(this.state.socket.readyState);
    this.state.socket.send(JSON.stringify(msg));
    this.setState({ message: "" });
  }

  render() {
    return (
      <div className="main">
        <div className="chatRoomWrapper">
          <h1 className="chatRoomHeader">Chat room</h1>
          <div className="messages" ref={c => (this.messagesDiv = c)}>
            {this.state.allMessages.map((msg, index) => (
              <div className="messageContainer" key={index}>
                <div className="messageInfo">
                  <span>{msg.user}</span>
                  <span>{msg.date}</span>
                </div>
                <hr className="hr" />
                <p>{msg.message}</p>
              </div>
            ))}
          </div>
          <div className="controls">
            <input
              className="textBox messageBox"
              disabled={this.state.socket.readyState !== 1}
              onKeyPress={this.enterKeyHandler}
              type="text"
              value={this.state.message}
              onChange={e => this.setState({ message: e.target.value })}
            />
            <button
              disabled={this.state.socket.readyState !== 1}
              className="btn"
              onClick={e => this.sendMessage(e)}
            >
              Send
            </button>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  chatRoomState: state.chatRoomState,
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({
  getAllChatRoomMessages: () => dispatch(fetchChatRoomMessages())
});

export default connect(mapStateToProps, mapDispatchToProps)(ChatRoomComponent);
