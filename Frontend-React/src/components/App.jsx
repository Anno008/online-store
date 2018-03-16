import React from "react";
import { Route, BrowserRouter as Router } from "react-router-dom";
import "css/app.css";
import ShoppingComponent from "./ShoppingComponent/ShoppingComponent";
import AuthComponent from "./AuthComponent/AuthComponent";

const App = () => (
  <div className="app">
    <div className="navBar" />
    <Router>
      <React.Fragment>
        <Route exact path="/" component={ShoppingComponent} />
        <Route path="/auth" component={AuthComponent} />
      </React.Fragment>
    </Router>
    <div className="footer" />
  </div>
);

export default App;
