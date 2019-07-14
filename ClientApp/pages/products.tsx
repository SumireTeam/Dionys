import React from 'react';
import {
  Breadcrumbs,
  Theme,
  makeStyles,
  createStyles,
} from '@material-ui/core';
import { Layout, Link, ProductList } from '../components';
import { ProductService, Product } from '../services';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    breadcrumb: {
      margin: theme.spacing(2, 0),
    },
  }),
);

interface Props {
  products: Product[];
}

const ProductsPage = ({ products }: Props) => {
  const classes = useStyles({});

  return (
    <Layout title="Product list">
      <Breadcrumbs className={classes.breadcrumb} aria-label="Breadcrumb">
        <Link href="/">Home</Link>
      </Breadcrumbs>

      <ProductList products={products} />
    </Layout>
  );
};

ProductsPage.getInitialProps = async () => {
  const service = new ProductService();
  const products = await service.list();
  return { products };
};

export default ProductsPage;
