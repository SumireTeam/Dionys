import React from 'react';
import { Breadcrumbs } from '@material-ui/core';
import { Layout, Link, ProductList } from '../../components';
import { ProductService, Product } from '../../services';

interface State {
  readonly products: Product[];
}

class ProductsPage extends React.Component<{}, State> {
  public constructor(props) {
    super(props);

    this.state = {
      products: [],
    };
  }

  public async componentDidMount() {
    const service = new ProductService();
    const products = await service.list();
    this.setState({ products });
  }

  public render() {
    return (
      <Layout title="Product list">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
        </Breadcrumbs>

        <ProductList products={this.state.products} />
      </Layout>
    );
  };
};

export default ProductsPage;
