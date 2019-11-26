import React from 'react';
import { Link as MuiLink } from '@material-ui/core';
import { Layout, Link } from '../components';

const Home = () => {
  return (
    <Layout title="Home">
      <ul>
        <li><Link to="/products">Products</Link></li>
        <li><Link to="/consumed">Consumed products</Link></li>
        <li><MuiLink href="/swagger/index.html" target="_blank">Swagger</MuiLink></li>
      </ul>
    </Layout>
  );
};

export default Home;
