import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";
import ComponentComponent from "./ComponentComponent";
import "components/css/catalog.css";

const ComponentListComponent = props => (
  <div className="catalog">
    {props.componentsState.data ?
    props.componentsState.data.components ?
      props.componentsState.data.components.map(c => <ComponentComponent key={c.id} component={c}/>) : null : null}
  </div>
  );

const mapStateToProps = state => ({
  pagingState: state.pagingState,
  componentsState: state.componentsState
});

const mapDispatchToProps = dispatch => ({
  handlePageNumberChanged: pageNumber => dispatch(pageNumberChanged(pageNumber)),
  handlePageChanged: pageSize => dispatch(pageSizeChanged(pageSize))
});

export default connect(mapStateToProps, mapDispatchToProps)(ComponentListComponent);
