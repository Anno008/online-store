import React from "react";

import ComponentsCatalogComponent from "components/ShoppingComponents/ComponentsCatalogComponent";
import AuthComponent from "../components/AuthComponent/AuthComponent";
import ComponentDetailsComponent from "../components/ShoppingComponents/ComponentDetailsComponents/ComponentDetailsComponent";
import ShoppingCartComponent from "../components/ShoppingCartComponents/ShoppingCartComponent";
import ChatRoomComponent from "../components/ChatRoomComponents/ChatRoomComponent";

const components = {
  catalog: <ComponentsCatalogComponent />,
  auth: <AuthComponent />,
  details: <ComponentDetailsComponent />,
  cart: <ShoppingCartComponent/>,
  chat: <ChatRoomComponent />
};

export const get = key => components[key] || console.log(`No components matching the key ${key}`);
