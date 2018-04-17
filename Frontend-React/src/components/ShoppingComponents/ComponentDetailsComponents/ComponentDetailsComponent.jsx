import React from "react";
import { connect } from "react-redux";

const ComponentDetailsComponent = props => (
    <React.Fragment>
    {props.componentState.data ? 
        <div className="main">
            {props.componentState.data.name}
            <br/>
            Price: {props.componentState.data.price}
            <br/>
            Brand: {props.componentState.data.brand.name}
            <br/>
            Type: {props.componentState.data.componentType.name}
        </div> :  props.componentState.isFetching ? "fetching"  : null }
        </React.Fragment>
  );

  const mapStateToProps = state => ({
    componentState: state.selectedComponentState
  });
  
  const mapDispatchToProps = dispatch => ({
  });
  
export default connect(mapStateToProps, mapDispatchToProps)(ComponentDetailsComponent);
