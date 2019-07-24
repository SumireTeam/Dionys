import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { Layout, Link, ProductList, LinkAdapter } from '../../components';
import { ProductService } from '../../services';
import { Product } from '../../models';

interface State {
  readonly loading: boolean;
  readonly products: Product[];
}

class List extends React.Component<{}, State> {
  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      products: [],
    };
  }

  public async componentDidMount() {
    const service = new ProductService();
    const products = await service.list();
    this.setState({ loading: false, products });
  }

  public render() {
    return (
      <Layout title="Product list">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            color="primary"
            component={LinkAdapter}
            to={'/products/create'}>Create</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ProductList products={this.state.products} />}
      </Layout>
    );
  };
};

export default List;
