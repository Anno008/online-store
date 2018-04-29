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
      {(props.brandsState.error || props.componentTypesState.error || props.componentsError) ? 
        <p className="error">{props.componentsError}</p>: null}
        <div className="filterContainer">
          <ComponentsNameFilterComponent />
          <BrandsFilterComponent />
          <ComponentTypesFilterComponent />
        </div>
    </React.Fragment>
  );
};

const mapStateToProps = state => ({
  brandsState: state.brandsState,
  componentsError: state.componentsState.error,
  componentTypesState: state.componentTypesState,
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
