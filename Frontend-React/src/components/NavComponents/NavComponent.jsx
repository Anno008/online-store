import React from "react";
import { connect } from "react-redux";
import "components/css/btn.css";
import { logout } from "actions/AuthActions";
import { navigationComponentChanged } from "../../actions/SelectedNavigationComponentActions";
import { keys } from "../../navigation/NavigationKeys";

const NavComponent = props => {
  const handleAuth = user => user && user.username ?
      props.logout() : props.changeNavigation(keys.auth);

    return (
      <div className="navBar">
        <button className="btn" onClick={() => props.changeNavigation(keys.catalog)}>
        Home
        </button>
        <div className="btnContainer">
        {props.userState.data && props.userState.data.username? 
        <React.Fragment>
          {props.userState.data.roles.indexOf("User") > -1 ? 
          <button className="btn" onClick={() => props.changeNavigation(keys.cart)}>Cart</button> : null}
          <button className="btn" onClick={() => props.changeNavigation(keys.chat)}>Chat room</button>
        </React.Fragment>: null}
        <button className="btn" onClick={() => handleAuth(props.userState.data)}>
        {props.userState.data && props.userState.data.username
        ? `${props.userState.data.username}, Logout`
        : "Login"}
        </button>
        </div>
      </div>
    );
};

const mapStateToProps = state => ({
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({
  logout: () => dispatch(logout()),
  changeNavigation: (key) => dispatch(navigationComponentChanged(key))
});

export default connect(mapStateToProps, mapDispatchToProps)(NavComponent);
