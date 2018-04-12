import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";

const ComponentListComponent = props => (
  <React.Fragment>
    {props.componentsState.data ?
    props.componentsState.data.components ?
       props.componentsState.data.components.map(c => <p key={c.id}>{c.name}</p>) : null : null}
  </React.Fragment>
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
