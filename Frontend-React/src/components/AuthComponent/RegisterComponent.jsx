import { login, register } from "../../actions/AuthActions";
import React from "react";
import { ToastContainer, toast, cssTransition } from "react-toastify";
import { connect } from "react-redux";
import "components/css/auth.css";
import "react-toastify/dist/ReactToastify.css";

const toastZoomTransition = cssTransition({
  enter: "zoomIn",
  exit: "zoomOut",
  duration: [500, 600]
});

class RegisterComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      username: "",
      password: "",
      confirmPassword: ""
    };
    this.handleChange = this.handleChange.bind(this);
    this.register = this.register.bind(this);
  }

  handleChange(e) {
    const { name, value } = e.target;
    this.setState({ [name]: value });
  }

  register(e) {
    e.preventDefault();
    const { username, password, confirmPassword } = this.state;
    if(password !== confirmPassword){
      toast.warn("Password does not match the confirm password!", {
        position: toast.POSITION.TOP_RIGHT,
        transition: toastZoomTransition,
        closeButton: false
      });
      return;
    }
    this.props.register(username, password);
  }


  render() {
    return (
      <form onSubmit={this.register} method="POST" onChange={this.handleChange}>
        <ToastContainer />
        <p className="textLabels">Username</p>
        <input
          className="textBox"
          name="username"
          required
          onChange={this.handleChange}
          value={this.state.username}
        />
        <br />
        <br />
        <p className="textLabels">Password</p>
        <input
          className="textBox"
          name="password"
          type="password"
          required
          onChange={this.handleChange}
          value={this.state.password}
        />
        <br />
        <br />
        <p className="textLabels">Confirm password</p>
        <input
          className="textBox"
          name="confirmPassword"
          type="confirmPassword"
          required
          onChange={this.handleChange}
          value={this.state.confirmPassword}
        />
        <br />
        <br />
        <input className="btn" type="Submit" defaultValue="Register" />
      </form>
    );
  }
}

const mapStateToProps = state => ({
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({
  register: (username, password) => dispatch(register(username, password))
});

export default connect(mapStateToProps, mapDispatchToProps)(RegisterComponent);
