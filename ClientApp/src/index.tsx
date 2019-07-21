import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';

import { Home, ProductList, ProductShow, ProductEdit } from './pages';

const rootElement = document.getElementById('root');
ReactDOM.render(
  <BrowserRouter>
    <CssBaseline />

    <Switch>
      <Route exact path="/"
        component={Home} />

      <Route exact path="/products"
        component={ProductList} />

      <Route exact path="/products/:id"
        render={props => <ProductShow {...props.match.params} />} />

      <Route exact path="/products/:id/edit"
        render={props => <ProductEdit {...props.match.params} history={props.history} />} />
    </Switch>
  </BrowserRouter>
  , rootElement);
