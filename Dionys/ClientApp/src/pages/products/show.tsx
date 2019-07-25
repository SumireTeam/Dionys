import React from 'react';
import { Breadcrumbs } from '@material-ui/core';
import { Layout, Link, ProductShow } from '../../components';
import { ProductService, Product } from '../../services';

interface Props {
  readonly id: string;
}

interface State {
  readonly product: Product;
}

class ProductPage extends React.Component<Props, State> {
  public constructor(props) {
    super(props);

    this.state = {
      product: null,
    };
  };

  public async componentDidMount() {
    const service = new ProductService();
    const product = await service.get(this.props.id);
    this.setState({ product });
  };

  public render() {
    return (
      <Layout title={this.state.product ? this.state.product.name : ''}>
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/products">Product list</Link>
        </Breadcrumbs>

        <ProductShow product={this.state.product} />
      </Layout>
    );
  };
}

export default ProductPage;
