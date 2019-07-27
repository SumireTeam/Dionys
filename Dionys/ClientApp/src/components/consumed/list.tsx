import React from 'react';
import {
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
} from '@material-ui/core';
import { Consumed } from '../../models';
import LinkAdapter from '../link-adapter';

interface Props {
  consumed: Consumed[];
  openDeleteDialog: (item: Consumed) => void;
}

class List extends React.Component<Props, {}> {
  public constructor(props) {
    super(props);

    this.state = {};
  }

  public render() {
    return (
      <Table className="list">
        <TableHead>
          <TableRow>
            <TableCell>Product</TableCell>
            <TableCell align="right">Weight&nbsp;(g)</TableCell>
            <TableCell>Date</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {this.props.consumed.map(consumed => (
            <TableRow key={consumed.id}>
              <TableCell component="th" scope="row">{consumed.productId}</TableCell>
              <TableCell align="right">{consumed.weight}</TableCell>
              <TableCell>{consumed.date.toISOString()}</TableCell>

              <TableCell className="list-actions">
                <Button className="button"
                  variant="contained"
                  size="small"
                  color="primary"
                  component={LinkAdapter}
                  to={`/consumed/${consumed.id}`}>View</Button>

                <Button className="button"
                  variant="contained"
                  size="small"
                  component={LinkAdapter}
                  to={`/consumed/${consumed.id}/edit`}>Edit</Button>

                <Button className="button"
                  variant="contained"
                  size="small"
                  color="secondary"
                  onClick={() => this.props.openDeleteDialog(consumed)}>Delete</Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    );
  }
}

export default List;
