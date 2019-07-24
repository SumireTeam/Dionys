import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { History } from 'history';
import { Layout, Link, ProductEdit, LinkAdapter } from '../../components';
import { ProductService } from '../../services';
import { Product } from '../../models';

interface Props {
  readonly id: string;
  readonly history: History;
}

interface State {
  readonly loading: boolean;
  readonly product: Product;
}

class Edit extends React.Component<Props, State> {
  protected readonly service: ProductService = new ProductService();

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      product: null,
    };

    this.onModelChange = this.onModelChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  };

  public async componentDidMount() {
    const product = await this.service.get(this.props.id);
    this.setState({ loading: false, product });
  };

  protected onModelChange(product: Product) {
    this.setState({ product });
  }

  protected async onSubmit() {
    const product = this.state.product.id
      ? await this.service.update(this.state.product)
      : await this.service.create(this.state.product);

    this.props.history.push(`/products/${product.id}`);
  }

  public render() {
    const product = this.state.product;

    return (
      <Layout title={product && product.name ? product.name : 'Edit product'}>
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/products">Product list</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            color="primary"
            component={LinkAdapter}
            to={`/products/${this.props.id}`}>View</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ProductEdit product={product}
            onModelChange={this.onModelChange}
            onSubmit={this.onSubmit} />}
      </Layout>
    );
  };
}

export default Edit;
