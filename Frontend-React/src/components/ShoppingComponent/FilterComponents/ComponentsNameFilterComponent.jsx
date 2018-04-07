import React from "react";
import { connect } from "react-redux";
import { searchBarTextChanged } from "actions/FilterActions";

const ComponentsNameFilterComponent = props => 
    <input placeholder="search" className="textBox" value={props.filterState.componentName} onChange={(e) => props.searchBarTextChanged(e.target.value)}/>;

const mapStateToProps = state => ({
    filterState: state.filterState
});

const mapDispatchToProps = dispatch => ({
    searchBarTextChanged: (name) => dispatch(searchBarTextChanged(name))
});

export default connect(mapStateToProps, mapDispatchToProps)(ComponentsNameFilterComponent);
