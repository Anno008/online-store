import React from "react";
import { connect } from "react-redux";
import "components/css/componentDetails.css";
import { addComponentToShoppingCart } from "../../../actions/ShoppingCartActions";

const ComponentDetailsComponent = props =>
  props.componentState.data ? (
    <React.Fragment>
      <div className="header">
        <div className="detailsHeaderContainer">
          <div>
            <p>Name: {props.componentState.data.name}</p>
            <p>Price: {props.componentState.data.price}</p>
            <p>Brand: {props.componentState.data.brand.name}</p>
            <p>Type: {props.componentState.data.componentType.name}</p>
          </div>
          <div>
            {props.userState.data && props.userState.data.username && props.userState.data.roles.indexOf("User") > -1? 
          <button className="btn" onClick={() => props.addToCart(props.componentState.data.id)}>Add to cart</button> : null}
          </div>
        </div>
      </div>
      <div className="main" >
        Comments, TODO
      </div>
    </React.Fragment>
  ) : props.componentState.isFetching ? (
    "fetching"
  ) : null;

const mapStateToProps = state => ({
  componentState: state.selectedComponentState,
  userState: state.userState
});

const mapDispatchToProps = dispatch => ({
  addToCart: (componentId) => dispatch(addComponentToShoppingCart(componentId))
});

export default connect(mapStateToProps, mapDispatchToProps)(
  ComponentDetailsComponent
);
