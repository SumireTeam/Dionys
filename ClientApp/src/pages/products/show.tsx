import React from 'react';
import { Breadcrumbs, CircularProgress } from '@material-ui/core';
import { Layout, Link, ProductShow } from '../../components';
import { ProductService, Product } from '../../services';

interface Props {
  readonly id: string;
}

interface State {
  readonly loading: boolean;
  readonly product: Product;
}

class ProductPage extends React.Component<Props, State> {
  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      product: null,
    };
  };

  public async componentDidMount() {
    const service = new ProductService();
    const product = await service.get(this.props.id);
    this.setState({ loading: false, product });
  };

  public render() {
    return (
      <Layout title={this.state.product ? this.state.product.name : ''}>
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/products">Product list</Link>
        </Breadcrumbs>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ProductShow product={this.state.product} />}
      </Layout>
    );
  };
}

export default ProductPage;
