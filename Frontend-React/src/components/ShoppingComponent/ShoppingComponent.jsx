import React from "react";
import { connect } from "react-redux";
import FilterComponent from "./FilterComponent";
import { fetchBrands } from "../../actions/BrandActions";
import apiCall from "../../api/ApiWrapper";

class ShoppingComponent extends React.Component {
  constructor(props) {
    super(props);
    props.initializeBrands();
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
  initializeBrands: () => dispatch(fetchBrands())
});

export default connect(mapStateToProps, mapDispatchToProps)(ShoppingComponent);
