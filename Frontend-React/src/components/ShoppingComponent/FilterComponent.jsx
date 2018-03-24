import React from "react";
import { connect } from "react-redux";

const FilterComponent = props => (
  <React.Fragment>
    <input />
    <select>
      <option>All</option>
      {props.brandsState.data
        ? props.brandsState.data.map(item => (
            <option key={item.id}>{item.name} </option>
          ))
        : null}
    </select>
  </React.Fragment>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
