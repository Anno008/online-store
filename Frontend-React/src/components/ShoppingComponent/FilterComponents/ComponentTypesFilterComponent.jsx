import React from "react";
import { connect } from "react-redux";
import { componentTypeSelectionChanged } from "actions/FilterActions";
import "components/css/dropdown.css";

const ComponentTypesFilterComponent = props => (
    <select className="dropdown" onChange={(e) => props.handleComponentTypeChange(e.currentTarget.value)}>
      <option value="0">All component types</option>
      {props.componentTypesState.data
        ? props.componentTypesState.data.map(item => (
            <option key={item.id} value={item.id}>{item.name} </option>
          ))
        : null}
    </select>
);

const mapStateToProps = state => ({
  componentTypesState: state.componentTypesState
});

const mapDispatchToProps = dispatch => ({
  handleComponentTypeChange: (typeId) => dispatch(componentTypeSelectionChanged(typeId))
});

export default connect(mapStateToProps, mapDispatchToProps)(ComponentTypesFilterComponent);
