import React from "react";
import ReactLogo from "assets/react.svg";
import ReduxLogo from "assets/redux.svg";
import { Route, BrowserRouter as Router } from "react-router-dom";
import "css/app.css";

const App = () => (
    <div className="app">
        <header className="app-header">
            <a href="https://reactjs.org/" target="_blank" rel="noopener noreferrer">
                <img className="app-logo" src={ReactLogo} alt="react-logo" />
            </a>
            <a href="https://redux.js.org/" target="_blank" rel="noopener noreferrer">
                <img className="app-logo-rev" src={ReduxLogo} alt="redux-logo" />
            </a>
            <h1 className="app-title">Welcome to React & Redux!</h1>
            <Router>
                <div>
                    <Route exact path="/" component={FirstComponent} />
                    <Route path="/auth" component={SecondComponent} />
                </div>
            </Router>
        </header>
    </div>);

const FirstComponent = () => (
    <div>
        <h1>First</h1>
    </div>
);

const SecondComponent = () => (
    <div>
        <h1>Second</h1>
    </div>
);

export default App;
