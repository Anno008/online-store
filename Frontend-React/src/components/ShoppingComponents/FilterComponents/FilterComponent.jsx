import React from "react";
import { connect } from "react-redux";
import BrandsFilterComponent from "./BrandsFilterComponent";
import ComponentsNameFilterComponent from "./ComponentsNameFilterComponent";
import ComponentTypesFilterComponent from "./ComponentTypesFilterComponent";
import { fetchComponents } from "actions/ComponentActions";
import "../../css/filter.css";

const FilterComponent = props => {
  return (
    <React.Fragment>
      {props.brandsState.error ? <p className="error">{props.brandsState.error}</p>: null}
      <div className="filterContainer">
        <ComponentsNameFilterComponent />
        <BrandsFilterComponent />
        <ComponentTypesFilterComponent />
      </div>
    </React.Fragment>
  );
};

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
