import React from "react";
import ReactLogo from "assets/react.svg";
import ReduxLogo from "assets/redux.svg";
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
        </header>
    </div>);

export default App;
