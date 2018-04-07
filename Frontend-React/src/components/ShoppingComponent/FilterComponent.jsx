import React from "react";
import { connect } from "react-redux";
import BrandsFilterComponent from "./FilterComponents/BrandsFilterComponent";
import ComponentsNameFilterComponent from "./FilterComponents/ComponentsNameFilterComponent";
import ComponentTypesFilterComponent from "./FilterComponents/ComponentTypesFilterComponent";
import { fetchComponents } from "actions/ComponentActions";
import "../css/filter.css";

const FilterComponent = (props) => {
  return (
   <React.Fragment >
     {props.brandsState.error ? <p>{props.brandsState.error}</p> :
      <div className="filterContainer">
        <ComponentsNameFilterComponent/>
        <BrandsFilterComponent/>
        <ComponentTypesFilterComponent/>
      </div>}
   </React.Fragment>
  );
};

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
