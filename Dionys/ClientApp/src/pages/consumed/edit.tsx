import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { History } from 'history';
import { Layout, Link, ConsumedEdit, LinkAdapter, DeleteDialog } from '../../components';
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

class Edit extends React.Component<Props, State> {
  protected readonly service = ServiceProvider.consumedService;

  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      consumed: null,
      deleteDialogOpen: false,
    };

    this.onModelChange = this.onModelChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.openDeleteDialog = this.openDeleteDialog.bind(this);
    this.dismissDeleteDialog = this.dismissDeleteDialog.bind(this);
    this.confirmDeleteDialog = this.confirmDeleteDialog.bind(this);
  };

  public async componentDidMount() {
    const consumed = await this.service.get(this.props.id);
    this.setState({ loading: false, consumed });
  };

  protected onModelChange(consumed: Consumed) {
    this.setState({ consumed });
  }

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

  protected async onSubmit() {
    const consumed = this.state.consumed.id
      ? await this.service.update(this.state.consumed)
      : await this.service.create(this.state.consumed);

    this.props.history.push(`/consumed/${consumed.id}`);
  }

  public render() {
    const consumed = this.state.consumed;

    return (
      <Layout title="Edit consumed product">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/consumed">Consumed list</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            color="primary"
            component={LinkAdapter}
            to={`/consumed/${this.props.id}`}>View</Button>

          {this.props.id
            ? <Button className="button"
              variant="contained"
              color="secondary"
              onClick={() => this.openDeleteDialog()}>Delete</Button>
            : null}
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ConsumedEdit consumed={consumed}
            onModelChange={this.onModelChange}
            onSubmit={this.onSubmit} />}

        <DeleteDialog open={this.state.deleteDialogOpen}
          title="Delete consumed product"
          text="Are you sure you want to delete consumed product?"
          onDismiss={this.dismissDeleteDialog}
          onConfirm={this.confirmDeleteDialog} />
      </Layout>
    );
  };
}

export default Edit;
