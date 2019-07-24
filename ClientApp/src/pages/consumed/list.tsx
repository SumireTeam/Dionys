import React from 'react';
import { Breadcrumbs, CircularProgress, Button } from '@material-ui/core';
import { Layout, Link, ConsumedList, LinkAdapter } from '../../components';
import { ConsumedService } from '../../services';
import { Consumed } from '../../models';

interface State {
  readonly loading: boolean;
  readonly consumed: Consumed[];
}

class List extends React.Component<{}, State> {
  public constructor(props) {
    super(props);

    this.state = {
      loading: true,
      consumed: [],
    };
  }

  public async componentDidMount() {
    const service = new ConsumedService();
    const consumed = await service.list();
    this.setState({ loading: false, consumed });
  }

  public render() {
    return (
      <Layout title="Consumed list">
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
          : <ConsumedList consumed={this.state.consumed} />}
      </Layout>
    );
  };
};

export default List;
