import React from 'react';
import { Layout, Link } from '../components';

const Home = () => {
  return (
    <Layout title="Home">
      <Link to="/products">Products</Link>
    </Layout>
  );
};

export default Home;
