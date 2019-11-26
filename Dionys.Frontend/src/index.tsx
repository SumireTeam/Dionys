import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';

import {
  ConsumedCreate,
  ConsumedEdit,
  ConsumedList,
  ConsumedShow,
  Home,
  ProductCreate,
  ProductEdit,
  ProductList,
  ProductShow,
} from './pages';

const rootElement = document.getElementById('root');
ReactDOM.render(
  <BrowserRouter>
    <CssBaseline />

    <Switch>
      <Route exact path="/"
        component={Home} />

      <Route exact path="/products"
        component={ProductList} />

      <Route exact path="/products/create"
        render={props => <ProductCreate history={props.history} />} />

      <Route exact path="/products/:id"
        render={props => <ProductShow {...props.match.params} history={props.history} />} />

      <Route exact path="/products/:id/edit"
        render={props => <ProductEdit {...props.match.params} history={props.history} />} />

      <Route exact path="/consumed"
        component={ConsumedList} />

      <Route exact path="/consumed/create"
        render={props => <ConsumedCreate history={props.history} />} />

      <Route exact path="/consumed/:id"
        render={props => <ConsumedShow {...props.match.params} history={props.history} />} />

      <Route exact path="/consumed/:id/edit"
        render={props => <ConsumedEdit {...props.match.params} history={props.history} />} />
    </Switch>
  </BrowserRouter>
  , rootElement);
