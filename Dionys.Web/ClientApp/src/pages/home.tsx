import React from 'react';
import { Layout, Link } from '../components';

const Home = () => {
  return (
    <Layout title="Home">
      <ul>
        <li><Link to="/products">Products</Link></li>
        <li><Link to="/consumed">Consumed products</Link></li>
        <li><a className="MuiTypography-root MuiLink-root MuiLink-underlineHover MuiTypography-colorPrimary"
          href="/swagger/index.html" target="_blank">Swagger</a></li>
      </ul>
    </Layout>
  );
};

export default Home;
