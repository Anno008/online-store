import React from "react";
import { connect } from "react-redux";
import BrandsFilterComponent from "./FilterComponents/BrandsFilterComponent";
import ComponentsNameFilterComponent from "./FilterComponents/ComponentsNameFilterComponent";
import ComponentTypesFilterComponent from "./FilterComponents/ComponentTypesFilterComponent";
import { fetchComponents } from "actions/ComponentActions";

const FilterComponent = props => {
  const fetchComponents = () => 
    props.fetchComponents(props.filterState, props.pagingState);  

  return (
   <React.Fragment >
     {props.brandsState.error ? <p>{props.brandsState.error}</p> :
      <React.Fragment>
        <ComponentsNameFilterComponent fetchComponents={fetchComponents}/>
        <BrandsFilterComponent fetchComponents={fetchComponents}/>
        <ComponentTypesFilterComponent fetchComponents={fetchComponents}/>
      </React.Fragment>}
   </React.Fragment>
  );
};

const mapStateToProps = state => ({
  brandsState: state.brandsState,
  filterState: state.filterState,
  pagingState: state.pagingState
});

const mapDispatchToProps = dispatch => ({
  fetchComponents: (filter, paging) => dispatch(fetchComponents(filter, paging))
});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
