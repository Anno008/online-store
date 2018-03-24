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
        onClick={() => handleAuth(props.userState.data)}
      >
        {props.userState.data && props.userState.data.username
          ? `Welcome ${props.userState.data.username}, Logout` : "Login"}
      </button>
    </div>
  );
};

const mapStateToProps = state => ({
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({
  logout: () => dispatch(logout())
});

export default connect(mapStateToProps, mapDispatchToProps)(NavComponent);