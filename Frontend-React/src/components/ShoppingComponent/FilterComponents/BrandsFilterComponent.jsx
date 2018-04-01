import React from "react";
import { connect } from "react-redux";
import { brandSelectionChanged } from "actions/FilterActions";

const BrandsFilterComponent = props => (
    <select onChange={(e) => props.handleBrandSelectionChanged(e.currentTarget.value)}>
      <option value="0">All</option>
      {props.brandsState.data
        ? props.brandsState.data.map(item => (
            <option key={item.id} value={item.id}>{item.name}</option>
          ))
        : null}
    </select>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({
  handleBrandSelectionChanged: (brandId) => dispatch(brandSelectionChanged(brandId))
});

export default connect(mapStateToProps, mapDispatchToProps)(BrandsFilterComponent);
