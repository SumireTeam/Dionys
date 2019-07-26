import React from 'react';
import { Layout, Link } from '../components';

const Home = () => {
  return (
    <Layout title="Home">
      <ul>
        <li><Link to="/products">Products</Link></li>
        <li><Link to="/consumed">Consumed</Link></li>
      </ul>
    </Layout>
  );
};

export default Home;
