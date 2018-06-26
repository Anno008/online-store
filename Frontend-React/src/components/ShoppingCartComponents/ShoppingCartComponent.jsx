import React from "react";
import { connect } from "react-redux";
import { removeComponentFromShoppingCart } from "actions/ShoppingCartActions";
import { fetchShoppingCart } from "../../actions/ShoppingCartActions";
import ComponentComponent from "../ShoppingComponents/ComponentComponents/ComponentComponent";
import { LoaderComponent } from "../Utils/LoaderComponent";
import "components/css/shoppingCart.css";

class ShoppingCartComponent extends React.Component {
  constructor(props) {
    super(props);
    props.fetchShoppingCart();
  }

  render() {
    return (
      this.props.shoppingCart.isFetching ? 
        <div className="main">
        <LoaderComponent/>
        </div> : this.props.shoppingCart.error ?
      <div className="header">
        <p className="error">{this.props.shoppingCart.error}</p> 
      </div> :
      <React.Fragment>
        <div className="header">
        <h1>Shopping cart, total price: {this.props.shoppingCart.data ? this.props.shoppingCart.data.totalPrice : 0}</h1>
          <h1>items {this.props.shoppingCart.data ? this.props.shoppingCart.data.items.length : 0}</h1>
        </div>
        <div className="main">
          {this.props.shoppingCart.data ? this.props.shoppingCart.data.items.map(c => 
            <div key={c.id} className="cartItemContainer">
              <ComponentComponent component={c.component} inCart={true}/>
              <button className="btn" onClick={() => this.props.removeComponentFromCart(c.id)}>Remove</button>
            </div>
          ) : null}
        </div> 
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => ({
    shoppingCart: state.shoppingCartState
});

const mapDispatchToProps = dispatch => ({
  removeComponentFromCart: componentId => dispatch(removeComponentFromShoppingCart(componentId)),
  fetchShoppingCart: () => dispatch(fetchShoppingCart())
});

export default connect(mapStateToProps, mapDispatchToProps)(
  ShoppingCartComponent
);
