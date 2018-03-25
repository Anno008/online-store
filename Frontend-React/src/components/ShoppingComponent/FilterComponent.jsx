import React from "react";
import { connect } from "react-redux";
import BrandsComponent from "./BrandsComponent";

const FilterComponent = props => (
  <React.Fragment>
    <input />
    <br />
    {props.brandsState.error ? <p>{props.brandsState.error}</p> :
    <React.Fragment>
       <BrandsComponent />
    </React.Fragment>}
  </React.Fragment>
);

const mapStateToProps = state => ({
  brandsState: state.brandsState
});

const mapDispatchToProps = dispatch => ({});

export default connect(mapStateToProps, mapDispatchToProps)(FilterComponent);
