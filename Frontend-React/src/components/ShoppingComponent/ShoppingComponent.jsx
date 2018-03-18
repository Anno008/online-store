import React from "react";
import { connect } from "react-redux";

const ShoppingComponent = () => (
  <React.Fragment>
    <div className="filter">Filter</div>
    <div className="main">Main</div>
  </React.Fragment>
);

const mapStateToProps = (state) => ({

});

const mapDispatchToProps = (dispatch) => ({

});

export default connect(mapStateToProps, mapDispatchToProps)(ShoppingComponent);
