import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { History } from 'history';
import { Layout, Link, ProductShow, LinkAdapter, DeleteDialog } from '../../components';
import { ServiceProvider } from '../../services';
import { Product } from '../../models';

interface Props {
  readonly id: string;
  readonly history: History;
}

interface State {
  readonly loading: boolean;
  readonly product: Product;
  readonly deleteDialogOpen: boolean;
}

class Show extends React.Component<Props, State> {
  protected readonly service = ServiceProvider.productService;

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      product: null,
      deleteDialogOpen: false,
    };

    this.openDeleteDialog = this.openDeleteDialog.bind(this);
    this.dismissDeleteDialog = this.dismissDeleteDialog.bind(this);
    this.confirmDeleteDialog = this.confirmDeleteDialog.bind(this);
  };

  public async componentDidMount() {
    const product = await this.service.get(this.props.id);

    this.setState({
      loading: false,
      product,
    });
  };

  protected openDeleteDialog() {
    this.setState({
      deleteDialogOpen: true,
    });
  }

  protected dismissDeleteDialog() {
    this.setState({
      deleteDialogOpen: false,
    });
  }

  protected async confirmDeleteDialog() {
    await this.service.delete(this.state.product.id);
    this.props.history.push('/products');
  }

  public render() {
    const product = this.state.product;

    return (
      <Layout title={product && product.name ? product.name : 'Product'}>
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/products">Product list</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            component={LinkAdapter}
            to={`/products/${this.props.id}/edit`}>Edit</Button>

          <Button className="button"
            variant="contained"
            color="secondary"
            onClick={() => this.openDeleteDialog()}>Delete</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ProductShow product={product} />}

        <DeleteDialog open={this.state.deleteDialogOpen}
          title="Delete product"
          text="Are you sure you want to delete product?"
          onDismiss={this.dismissDeleteDialog}
          onConfirm={this.confirmDeleteDialog} />
      </Layout>
    );
  };
}

export default Show;
