import React from "react";
import { login, register, checkForUser } from "../../actions/AuthActions";
import { connect } from "react-redux";
import LoginComponent from "./LoginComponent";
import RegisterComponent from "./RegisterComponent";
import "components/css/label.css";
import { navigationComponentChanged } from "../../actions/SelectedNavigationComponentActions";
import { keys } from "navigation/NavigationKeys";


class AuthComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = { login:  true};
    if (this.props.userState.data) {
      this.props.changeNavigation(keys.catalog);
    }
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

const mapDispatchToProps = dispatch => ({
  checkStatus: () => dispatch(checkForUser()),
  changeNavigation: (key) => dispatch(navigationComponentChanged(key))
});

export default connect(mapStateToProps, mapDispatchToProps)(AuthComponent);
