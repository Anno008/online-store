import React from "react";
import { connect } from "react-redux";
import "css/app.css";
import AuthComponent from "./AuthComponent/AuthComponent";
import NavComponent from "./NavComponents/NavComponent";
import { checkForUser } from "../actions/AuthActions";
import ComponentsCatalogComponent from "components/ShoppingComponents/ComponentsCatalogComponent";
import { get } from "../navigation/NavigationComponentsDictionary";

class App extends React.Component {
  constructor(props) {
    super(props);
    props.checkIfTheUserWasLoggedIn();
  }

  render() {
    return (
      <div className="app">
        <NavComponent  />
        {get(this.props.selectedNavigationComponent.data)}
        <div className="footer" />
      </div>
    );
  }
}

const mapStateToProps = state => ({
  selectedNavigationComponent: state.selectedNavigationComponent
});

const mapDispatchToProps = dispatch => ({
  checkIfTheUserWasLoggedIn: () => dispatch(checkForUser())
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
