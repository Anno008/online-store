import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";

const ComponentComponent = props => (
    props.component ? 
        <div className="component">
            {props.component.name}
            <br/>
            Price: {props.component.price}
            <br/>
            Brand: {props.component.brand.name}
            <br/>
            Type: {props.component.componentType.name}
        </div> : null
  );

export default ComponentComponent;
