import React from "react";
import { connect } from "react-redux";
import BrandsFilterComponent from "./FilterComponents/BrandsFilterComponent";
import ComponentsNameFilterComponent from "./FilterComponents/ComponentsNameFilterComponent";

const FilterComponent = props => (
  <React.Fragment>
    {props.brandsState.error ? <p>{props.brandsState.error}</p> :
    <React.Fragment>
       <ComponentsNameFilterComponent />
       <BrandsFilterComponent />
    </React.Fragment>}
  </React.Fragment>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
