import React from 'react';
import {
  Breadcrumbs,
  CircularProgress,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
} from '@material-ui/core';
import { Layout, Link, ProductList, LinkAdapter } from '../../components';
import { ServiceProvider } from '../../services';
import { Product } from '../../models';

interface State {
  readonly loading: boolean;
  readonly products: Product[];
  readonly deleteDialogItem: null | Product;
  readonly deleteDialogOpen: boolean;
}

class List extends React.Component<{}, State> {
  protected readonly service = ServiceProvider.productService;

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      products: [],
      deleteDialogItem: null,
      deleteDialogOpen: false,
    };

    this.openDeleteDialog = this.openDeleteDialog.bind(this);
    this.dismissDeleteDialog = this.dismissDeleteDialog.bind(this);
    this.confirmDeleteDialog = this.confirmDeleteDialog.bind(this);
  }

  public async componentDidMount() {
    const products = await this.service.list();

    this.setState({
      loading: false,
      products,
    });
  }

  protected openDeleteDialog(item: Product) {
    this.setState({
      deleteDialogItem: item,
      deleteDialogOpen: true,
    });
  }

  protected dismissDeleteDialog() {
    this.setState({
      deleteDialogItem: null,
      deleteDialogOpen: false,
    });
  }

  protected async confirmDeleteDialog() {
    const id = this.state.deleteDialogItem.id;
    await this.service.delete(id);

    this.setState({
      products: this.state.products.filter(item => item.id !== id),
      deleteDialogItem: null,
      deleteDialogOpen: false,
    });
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
          : <ProductList products={this.state.products} openDeleteDialog={this.openDeleteDialog} />}

        <Dialog open={this.state.deleteDialogOpen} onClose={e => this.dismissDeleteDialog()}>
          <DialogTitle>Delete product</DialogTitle>

          <DialogContent>
            <DialogContentText>
              Are you sure you want to delete product?
            </DialogContentText>
          </DialogContent>

          <DialogActions>
            <Button onClick={e => this.dismissDeleteDialog()} color="primary">No</Button>
            <Button onClick={e => this.confirmDeleteDialog()} color="primary" autoFocus>Yes</Button>
          </DialogActions>
        </Dialog>
      </Layout>
    );
  };
};

export default List;
