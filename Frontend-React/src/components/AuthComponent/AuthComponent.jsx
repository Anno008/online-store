import React from "react";
import { login, register } from "../../actions/AuthActions";
import { connect } from "react-redux";
import LoginComponent from "./LoginComponent";
import RegisterComponent from "./RegisterComponent";
import "components/css/label.css";

class AuthComponent extends React.Component {
  constructor(props) {
    super(props);
    if (props.userState.data) {
      location.href = "/";
    }
  }

  componentWillMount() {
    this.setState({
      login: true
    });
  }

  handleLinkClick(e, login) {
    this.setState({
      login: login
    });
  }

  render() {
    return (
      <React.Fragment>
        <div className="header">
          <p>
            {this.props.userState.error ? this.props.userState.error : null}
          </p>
        </div>
        <div className="main">
          {this.state.login ? <LoginComponent /> : <RegisterComponent />}
          {this.state.login ? 
            <label
              className="simpleLabel"
              onClick={e => this.handleLinkClick(e, false)}>
              Don't have an account
            </label>
              : 
            <label
              className="simpleLabel"
              onClick={e => this.handleLinkClick(e, true)}>
              Already have an account? Sign in
            </label>
          }
        </div>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => ({
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(AuthComponent);
