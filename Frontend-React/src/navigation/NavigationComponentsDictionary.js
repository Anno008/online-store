import React from "react";

import ComponentsCatalogComponent from "components/ShoppingComponents/ComponentsCatalogComponent";
import AuthComponent from "../components/AuthComponent/AuthComponent";
import ComponentDetailsComponent from "../components/ShoppingComponents/ComponentDetailsComponents/ComponentDetailsComponent";

const components = {
  catalog: <ComponentsCatalogComponent />,
  auth: <AuthComponent />,
  details: <ComponentDetailsComponent />
};

export const get = key => components[key] || console.log(`No components matching the key ${key}`);
