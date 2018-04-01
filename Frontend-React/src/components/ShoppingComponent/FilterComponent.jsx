import React from "react";
import { connect } from "react-redux";
import BrandsFilterComponent from "./FilterComponents/BrandsFilterComponent";
import ComponentsNameFilterComponent from "./FilterComponents/ComponentsNameFilterComponent";
import ComponentTypesFilterComponent from "./FilterComponents/ComponentTypesFilterComponent";
import { fetchComponents } from "actions/ComponentActions";

class FilterComponent extends React.Component {
  constructor(props){
    super(props);
  }

  componentWillReceiveProps(props) {
    // checking if the new props differ from the old ones, if they do make a get request
    if(props.filterState.brandId !== this.props.filterState.brandId ||
      props.filterState.componentTypeId !== this.props.filterState.componentTypeId ||
      props.filterState.componentName !== this.props.filterState.componentName ||
      props.pagingState.page !== this.props.pagingState.page ||
      props.pagingState.pageSize !== this.props.pagingState.pageSize){
        props.fetchComponents(props.filterState, props.pagingState);
    }
  }

  render() {
  return (
   <React.Fragment >
     {this.props.brandsState.error ? <p>{this.props.brandsState.error}</p> :
      <React.Fragment>
        <ComponentsNameFilterComponent/>
        <BrandsFilterComponent/>
        <ComponentTypesFilterComponent/>
      </React.Fragment>}
   </React.Fragment>
  );
}
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
