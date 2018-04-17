import React from "react";
import { Route, BrowserRouter as Router } from "react-router-dom";
import { connect } from "react-redux";
import "css/app.css";
import AuthComponent from "./AuthComponent/AuthComponent";
import NavComponent from "./NavComponent/NavComponent";
import { checkForUser } from "../actions/AuthActions";
import ComponentsCatalogComponent from "components/ShoppingComponents/ComponentsCatalogComponent";

class App extends React.Component {
  constructor(props) {
    super(props);
    props.checkIfTheUserWasLoggedIn();
  }

  render() {
    return (
      <div className="app">
        <NavComponent />
        <Router>
          <React.Fragment>
            <Route exact path="/" component={ComponentsCatalogComponent} />
            <Route path="/auth" component={AuthComponent} />
          </React.Fragment>
        </Router>
        <div className="footer" />
      </div>
    );
  }
}

const mapStateToProps = state => ({});

const mapDispatchToProps = dispatch => ({
  checkIfTheUserWasLoggedIn: () => dispatch(checkForUser())
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
