import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { History } from 'history';
import { Layout, Link, ConsumedShow, LinkAdapter, DeleteDialog } from '../../components';
import { ServiceProvider } from '../../services';
import { Consumed } from '../../models';

interface Props {
  readonly id: string;
  readonly history: History;
}

interface State {
  readonly loading: boolean;
  readonly consumed: Consumed;
  readonly deleteDialogOpen: boolean;
}

class Show extends React.Component<Props, State> {
  protected readonly service = ServiceProvider.consumedService;

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      consumed: null,
      deleteDialogOpen: false,
    };

    this.openDeleteDialog = this.openDeleteDialog.bind(this);
    this.dismissDeleteDialog = this.dismissDeleteDialog.bind(this);
    this.confirmDeleteDialog = this.confirmDeleteDialog.bind(this);
  };

  public async componentDidMount() {
    const consumed = await this.service.get(this.props.id);

    this.setState({
      loading: false,
      consumed,
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
    await this.service.delete(this.state.consumed.id);
    this.props.history.push('/consumed');
  }

  public render() {
    const consumed = this.state.consumed;

    return (
      <Layout title="Consumed product">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/consumed">Consumed list</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            component={LinkAdapter}
            to={`/consumed/${this.props.id}/edit`}>Edit</Button>

          <Button className="button"
            variant="contained"
            color="secondary"
            onClick={() => this.openDeleteDialog()}>Delete</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ConsumedShow consumed={consumed} />}

        <DeleteDialog open={this.state.deleteDialogOpen}
          title="Delete consumed product"
          text="Are you sure you want to delete consumed product?"
          onDismiss={this.dismissDeleteDialog}
          onConfirm={this.confirmDeleteDialog} />
      </Layout>
    );
  };
}

export default Show;
