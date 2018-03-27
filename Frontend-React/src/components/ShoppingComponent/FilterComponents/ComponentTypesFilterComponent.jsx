import React from "react";
import { connect } from "react-redux";

const BrandsFilterComponent = props => (
    <select>
      <option>All</option>
      {props.componentTypesState.data
        ? props.componentTypesState.data.map(item => (
            <option key={item.id}>{item.name} </option>
          ))
        : null}
    </select>
);

const mapStateToProps = state => ({
  componentTypesState: state.componentTypesState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(BrandsFilterComponent);
