import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { Layout, Link, ConsumedShow, LinkAdapter } from '../../components';
import { ServiceProvider } from '../../services';
import { Consumed } from '../../models';

interface Props {
  readonly id: string;
}

interface State {
  readonly loading: boolean;
  readonly consumed: Consumed;
}

class Show extends React.Component<Props, State> {
  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      consumed: null,
    };
  };

  public async componentDidMount() {
    const service = ServiceProvider.consumedService;
    const consumed = await service.get(this.props.id);
    this.setState({ loading: false, consumed });
  };

  public render() {
    const consumed = this.state.consumed;

    return (
      <Layout title="Consumed">
        <Breadcrumbs className="breadcrumbs" aria-label="Breadcrumb">
          <Link to="/">Home</Link>
          <Link to="/consumed">Consumed list</Link>
        </Breadcrumbs>

        <div className="actions">
          <Button className="button"
            variant="contained"
            component={LinkAdapter}
            to={`/consumed/${this.props.id}/edit`}>Edit</Button>
        </div>

        {this.state.loading
          ? <CircularProgress className="progress" />
          : <ConsumedShow consumed={consumed} />}
      </Layout>
    );
  };
}

export default Show;
