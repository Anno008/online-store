import React from "react";
import { connect } from "react-redux";

const BrandsComponent = props => (
    <select>
      <option>All</option>
      {props.brandsState.data
        ? props.brandsState.data.map(item => (
            <option key={item.id}>{item.name} </option>
          ))
        : null}
    </select>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(BrandsComponent);
