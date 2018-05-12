import React from "react";
import { connect } from "react-redux";
import { componentSelected } from "actions/ComponentActions";

const ComponentComponent = props =>
  props.component ? 
    <div
      className="component"
      onClick={() => props.inCart ? null : props.componentSelected(props.component.id)}>
      {props.component.name}
      <br />
      Price: {props.component.price}
      <br />
      Brand: {props.component.brand.name}
      <br />
      Type: {props.component.componentType.name}
    </div> : null

const mapStateToProps = state => ({});

const mapDispatchToProps = dispatch => ({
  componentSelected: id => dispatch(componentSelected(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(ComponentComponent);
