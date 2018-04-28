import React from "react";
import { connect } from "react-redux";
import FilterComponent from "./FilterComponents/FilterComponent";
import { fetchBrands } from "../../actions/BrandActions";
import { fetchComponentTypes } from "../../actions/ComponentTypeActions";
import apiCall from "../../api/ApiWrapper";
import { fetchComponents } from "actions/ComponentActions";
import ComponentListComponent from "./ComponentComponents/ComponentListComponent";
import PagingComponent from "./PagingComponents/PagingComponent";
import { debounce } from "lodash";
import ComponentDetailsComponent from "./ComponentDetailsComponents/ComponentDetailsComponent";
import { LoaderComponent } from "../Utils/LoaderComponent";

class ComponentsCatalogComponent extends React.Component {
  constructor(props) {
    super(props);
    this.props.initializeBrands();
    this.props.initializeComponentTypes();
    props.fetchComponents(this.props.filterState, this.props.pagingState);
  }

  componentWillReceiveProps(props) {
    // checking if the new props differ from the old ones, if they do make a get request
    if(props.filterState.brandId !== this.props.filterState.brandId ||
      props.filterState.componentTypeId !== this.props.filterState.componentTypeId ||
      props.filterState.componentName !== this.props.filterState.componentName ||
      props.pagingState.page !== this.props.pagingState.page ||
      props.pagingState.pageSize !== this.props.pagingState.pageSize){
        this.props.fetchComponentsWithDelay(props, 100);
    }
  }

  render() {
    return (
        <React.Fragment>
          <div className="header">
            <FilterComponent />
          </div>
          <div className="main">
            <PagingComponent />
            {this.props.componentsState.isFetching ? <LoaderComponent/> : <ComponentListComponent />}
          </div>
        </React.Fragment>
    );
  }
}

const mapStateToProps = state => ({
  brandsState: state.brandsState,
  filterState: state.filterState,
  pagingState: state.pagingState,
  selectedComponent: state.selectedComponentState,
  componentsState: state.componentsState
});

const updateWithDelay = debounce((dispatch, props) => 
  dispatch(fetchComponents(props.filterState, props.pagingState)), 800);

const mapDispatchToProps = dispatch => ({
  initializeBrands: () => dispatch(fetchBrands()),
  initializeComponentTypes: () => dispatch(fetchComponentTypes()),
  fetchComponents: (filter, paging) => dispatch(fetchComponents(filter, paging)),
  fetchComponentsWithDelay: (props) => updateWithDelay(dispatch, props)
  
});

export default connect(mapStateToProps, mapDispatchToProps)(ComponentsCatalogComponent);
