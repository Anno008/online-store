import React from "react";
import { connect } from "react-redux";
import "components/NavComponent/nav.css";
import { logout } from "../../actions/AuthActions";
import { redirectUri } from "../../constants";

const NavComponent = props => {
  const handleAuth = userExists => {
    if (userExists) {
      props.logout();
    } else {
      location.href = redirectUri;
    }
  };

  return (
    <div className="navBar">
      <button
        className="userInfo"
        onClick={() => handleAuth(props.authState.data)}
      >
        {props.authState.data && props.authState.data.username
          ? `Welcome ${props.authState.data.username}, Logout` : "Login"}
      </button>
    </div>
  );
};

const mapStateToProps = state => ({
  authState: state.authState
});

const mapDispatchToProps = dispatch => ({
  logout: () => dispatch(logout())
});

export default connect(mapStateToProps, mapDispatchToProps)(NavComponent);
