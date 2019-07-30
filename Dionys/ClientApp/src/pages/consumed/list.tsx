import React from 'react';
import {
  Breadcrumbs,
  CircularProgress,
  Button,
} from '@material-ui/core';
import { Layout, Link, ConsumedList, LinkAdapter, DeleteDialog } from '../../components';
import { ServiceProvider } from '../../services';
import { Consumed } from '../../models';

interface State {
  readonly loading: boolean;
  readonly consumed: Consumed[];
  readonly deleteDialogItem: null | Consumed;
  readonly deleteDialogOpen: boolean;
}

class List extends React.Component<{}, State> {
  protected readonly service = ServiceProvider.consumedService;

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      consumed: [],
      deleteDialogItem: null,
      deleteDialogOpen: false,
    };

    this.openDeleteDialog = this.openDeleteDialog.bind(this);
    this.dismissDeleteDialog = this.dismissDeleteDialog.bind(this);
    this.confirmDeleteDialog = this.confirmDeleteDialog.bind(this);
  }

  public async componentDidMount() {
    const consumed = await this.service.list();

    this.setState({
      loading: false,
      consumed,
    });
  }

  protected openDeleteDialog(item: Consumed) {
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
      consumed: this.state.consumed.filter(item => item.id !== id),
      deleteDialogItem: null,
      deleteDialogOpen: false,
    });
  }

  public render() {
    return (
      <Layout title="Consumed product list">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            color="primary"
            component={LinkAdapter}
            to={'/consumed/create'}>Create</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ConsumedList consumed={this.state.consumed} openDeleteDialog={this.openDeleteDialog} />}

        <DeleteDialog open={this.state.deleteDialogOpen}
          title="Delete consumed product"
          text="Are you sure you want to delete consumed product?"
          onDismiss={this.dismissDeleteDialog}
          onConfirm={this.confirmDeleteDialog} />
      </Layout>
    );
  };
};

export default List;
