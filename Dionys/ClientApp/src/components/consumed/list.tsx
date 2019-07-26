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
}

const List = ({ consumed }: Props) => {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Product</TableCell>
          <TableCell align="right">Weight&nbsp;(g)</TableCell>
          <TableCell>Date</TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {consumed.map(consumed => (
          <TableRow key={consumed.id}>
            <TableCell component="th" scope="row">{consumed.productId}</TableCell>
            <TableCell align="right">{consumed.weight}</TableCell>
            <TableCell>{consumed.date.toISOString()}</TableCell>

            <TableCell>
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
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};

export default List;
