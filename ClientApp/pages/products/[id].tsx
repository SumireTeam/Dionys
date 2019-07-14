import React from 'react';
import {
  Breadcrumbs,
  Theme,
  makeStyles,
  createStyles,
} from '@material-ui/core';
import { Layout, Link, ProductShow } from '../../components';
import { ProductService, Product } from '../../services';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    breadcrumb: {
      margin: theme.spacing(2, 0),
    },
  }),
);

interface Props {
  product: Product;
}

const ProductPage = ({ product }: Props) => {
  const classes = useStyles({});

  return (
    <Layout title={product.name}>
      <Breadcrumbs className={classes.breadcrumb} aria-label="Breadcrumb">
        <Link href="/">Home</Link>
        <Link href="/products">Product list</Link>
      </Breadcrumbs>

      <ProductShow product={product} />
    </Layout>
  );
};

ProductPage.getInitialProps = async (context) => {
  const { id } = context.query;
  const service = new ProductService();
  const product = await service.get(id);
  return { product };
};

export default ProductPage;
