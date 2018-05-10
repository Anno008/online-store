import React from "react";
import { connect } from "react-redux";
import { pageNumberChanged, pageSizeChanged } from "actions/PagingActions";
import "components/css/paging.css";

const PagingComponent = props => (
  <div className="pagingContainer">
    Items per page
    <select
      id="itemsPerPage"
      disabled={props.componentsState.isFetching || false}
      className="dropdown itemsParePage horizontalPadding"
      value={props.pagingState.pageSize}
      onChange={e => props.handlePageSizeChanged(e.target.value)}>
      <option value={1}>1</option>
      <option value={5}>5</option>
      <option value={10}>10</option>
      <option value={15}>15</option>
    </select>
    Total items: {props.componentsState ?  props.componentsState.data ? props.componentsState.data.totalItems : 0 : 0 || 0}
    <input
        type="button" 
        className="horizontalPadding"
        disabled={props.componentsState == undefined  || props.componentsState.data == undefined || props.componentsState.data.totalItems == undefined || props.componentsState.data.currentPage <= 1}
        onClick={() => props.handlePageNumberChanged(props.componentsState.data.currentPage - 1)}
        value="&lt;&lt;"/>
      {props.componentsState ? props.componentsState.data ? props.componentsState.data.currentPage : 1 : 1 || 1} 
      / 
      {props.componentsState ? props.componentsState.data ? props.componentsState.data.pages : 1 : 1 || 1}
    <input 
        type="button"
        className="horizontalPadding"
        disabled={props.componentsState == undefined || props.componentsState.data == undefined || props.componentsState.data.currentPage == props.componentsState.data.pages}
        onClick={() => props.handlePageNumberChanged(props.componentsState.data.currentPage + 1)}
        value="&gt;&gt;"/>
  </div>
);

const mapStateToProps = state => ({
  pagingState: state.pagingState,
  componentsState: state.componentsState
});

const mapDispatchToProps = dispatch => ({
  handlePageNumberChanged: pageNumber => dispatch(pageNumberChanged(pageNumber)),
  handlePageSizeChanged: pageSize => dispatch(pageSizeChanged(pageSize))
});

export default connect(mapStateToProps, mapDispatchToProps)(PagingComponent);
