import React from "react";
import { login } from "../../actions/AuthActions";
import { connect } from "react-redux";
import "components/css/textBox.css";
import "components/css/btn.css";

class LoginComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      username: "",
      password: "",
      rememberMe: false
    };
    this.handleChange = this.handleChange.bind(this);
    this.login = this.login.bind(this);
  }

  handleChange(e) {
    const { name, value } = e.target;
    this.setState({ [name]: value });
  }

  login(e) {
    e.preventDefault();
    const { username, password, rememberMe } = this.state;
    this.props.login(username, password, rememberMe);
  }

  render() {
    return (
      <form onSubmit={this.login} method="POST" onChange={this.handleChange} className="auth">
        <p className="textLabels">Username</p>
        <input
          className="textBox"
          name="username"
          required
          onChange={this.handleChange}
          value={this.state.username}
        />
        <br/>
        <br/>
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
        <label >
          Remember me
          <input
            type="checkbox"
            onChange={this.handleChange}
            value={this.state.rememberMe}
          />
        </label>
        <br />
        <br />
        <input className="btn" type="Submit" defaultValue="Login" />
      </form>
    );
  }
}

const mapStateToProps = state => ({});

const mapDispatchToProps = dispatch => ({
  login: (username, password, rememberMe) => dispatch(login(username, password, rememberMe))
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginComponent);
