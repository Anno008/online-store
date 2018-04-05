import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";

const ComponentListComponent = props => (
  <React.Fragment>
    <div>
    Items per page  
    <select value={props.pagingState.pageSize} onChange={(e) => props.handlePageChanged(e.target.value)}>
      <option value={1}>1</option>
      <option value={5}>5</option>
      <option value={10}>10</option>
      <option value={15}>15</option>
    </select>
    {props.componentsState.data ? 
     <label>
      Current page
      {props.componentsState.data.currentPage}
      Total amount of items:
      {props.componentsState.data.totalItems}
      Total pages
      {props.componentsState.data.pages}
    </label> : null}
    </div>
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
