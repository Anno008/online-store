import React from "react";
import { connect } from "react-redux";
import { componentTypeSelectionChanged } from "actions/FilterActions";

const ComponentTypesFilterComponent = props => (
    <select onChange={(e) => props.handleComponentTypeChange(e.currentTarget.value)}>
      <option value="0">All</option>
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
