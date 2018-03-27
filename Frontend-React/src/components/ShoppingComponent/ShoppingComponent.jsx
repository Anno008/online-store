import React from "react";
import { connect } from "react-redux";
import FilterComponent from "./FilterComponent";
import { fetchBrands } from "../../actions/BrandActions";
import { fetchComponentTypes } from "../../actions/ComponentTypeActions";
import apiCall from "../../api/ApiWrapper";

class ShoppingComponent extends React.Component {
  constructor(props) {
    super(props);
    props.initializeBrands();
    props.initializeComponentTypes();
  }

  render() {
    return (
      <React.Fragment>
        <div className="filter">
          <FilterComponent />
        </div>
        <div className="main">Main</div>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => ({});

const mapDispatchToProps = dispatch => ({
  initializeBrands: () => dispatch(fetchBrands()),
  initializeComponentTypes: () => dispatch(fetchComponentTypes())
});

export default connect(mapStateToProps, mapDispatchToProps)(ShoppingComponent);
