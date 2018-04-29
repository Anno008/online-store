import React from "react";
import { connect } from "react-redux";
import { brandSelectionChanged } from "actions/FilterActions";
import "components/css/dropdown.css";

const BrandsFilterComponent = props => (
    <select 
    disabled={props.brandsState.isFetching}
    value={props.filterState.brandId} 
    className="dropdown" 
    onChange={(e) => props.handleBrandSelectionChanged(e.currentTarget.value)}>
      <option value="0">All brands</option>
      {props.brandsState.data
        ? props.brandsState.data.map(item => (
            <option key={item.id} value={item.id}>{item.name}</option>
          ))
        : null}
    </select>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState,
  filterState: state.filterState
});

const mapDispatchToProps = dispatch => ({
  handleBrandSelectionChanged: (brandId) => dispatch(brandSelectionChanged(brandId))
});

export default connect(mapStateToProps, mapDispatchToProps)(BrandsFilterComponent);
