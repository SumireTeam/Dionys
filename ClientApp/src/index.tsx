import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';

import Index from './pages/index';
import ProductList from './pages/products/list';
import ProductShow from './pages/products/show';

const rootElement = document.getElementById('root');
ReactDOM.render(
  <BrowserRouter>
    <CssBaseline />

    <Switch>
      <Route exact path="/" component={Index} />
      <Route exact path="/products" component={ProductList} />
      <Route path="/products/:id" render={props => <ProductShow {...props.match.params} />} />
    </Switch>
  </BrowserRouter>
  , rootElement);
