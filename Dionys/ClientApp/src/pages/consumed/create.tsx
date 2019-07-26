import React from 'react';
import { Breadcrumbs } from '@material-ui/core';
import { History } from 'history';
import { Layout, Link, ConsumedEdit } from '../../components';
import { ServiceProvider } from '../../services';
import { Consumed } from '../../models';

interface Props {
  readonly history: History;
}

interface State {
  readonly consumed: Consumed;
}

class Create extends React.Component<Props, State> {
  protected readonly service = ServiceProvider.consumedService;

  public constructor(props) {
    super(props);

    this.state = {
      consumed: null,
    };

    this.onModelChange = this.onModelChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  };

  public async componentDidMount() {
    const consumed: Consumed = {
      id: null,
      productId: '',
      weight: 0,
      date: new Date(),
    };

    this.setState({ consumed });
  };

  protected onModelChange(consumed: Consumed) {
    this.setState({ consumed });
  }

  protected async onSubmit() {
    const data = { ...this.state.consumed };
    delete data.id;

    const consumed = await this.service.create(data);
    this.props.history.push(`/consumed/${consumed.id}`);
  }

  public render() {
    const consumed = this.state.consumed;

    return (
      <Layout title="Create consumed">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/consumed">Consumed list</Link>
        </Breadcrumbs>

        <ConsumedEdit consumed={consumed}
          onModelChange={this.onModelChange}
          onSubmit={this.onSubmit} />
      </Layout>
    );
  };
}

export default Create;
