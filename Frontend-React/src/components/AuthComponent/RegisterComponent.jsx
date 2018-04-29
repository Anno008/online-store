import { login, register } from "../../actions/AuthActions";
import React from "react";
import { connect } from "react-redux";
import "components/css/auth.css";

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
    console.log(password);
    console.log(confirmPassword);
    if(password !== confirmPassword){
      alert("Password doesn't match")
      return;
    }
    this.props.register(username, password);
  }


  render() {
    return (
      <form onSubmit={this.register} method="POST" onChange={this.handleChange}>
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
