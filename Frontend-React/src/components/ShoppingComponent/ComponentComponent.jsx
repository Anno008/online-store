import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";

const ComponentComponent = props => (
    props.component ? 
        <div className="component">
            {props.component.name}
            <br/>
            {props.component.price}
            <br/>
            {props.component.brand.name}
            <br/>
            {props.component.componentType.name}
        </div> : null
  );

export default ComponentComponent;
