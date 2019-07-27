import React from 'react';
import {
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
} from '@material-ui/core';
import { Product } from '../../models';
import LinkAdapter from '../link-adapter';

interface Props {
  products: Product[];
  openDeleteDialog: (item: Product) => void;
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
            <TableCell>Name</TableCell>
            <TableCell align="right">Protein&nbsp;(g)</TableCell>
            <TableCell align="right">Fat&nbsp;(g)</TableCell>
            <TableCell align="right">Carbs&nbsp;(g)</TableCell>
            <TableCell align="right">Calories</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {this.props.products.map(product => (
            <TableRow key={product.id}>
              <TableCell component="th" scope="row">{product.name}</TableCell>
              <TableCell align="right">{product.protein}</TableCell>
              <TableCell align="right">{product.fat}</TableCell>
              <TableCell align="right">{product.carbs}</TableCell>
              <TableCell align="right">{product.calories}</TableCell>

              <TableCell className="list-actions">
                <Button className="button"
                  variant="contained"
                  size="small"
                  color="primary"
                  component={LinkAdapter}
                  to={`/products/${product.id}`}>View</Button>

                <Button className="button"
                  variant="contained"
                  size="small"
                  component={LinkAdapter}
                  to={`/products/${product.id}/edit`}>Edit</Button>

                <Button className="button"
                  variant="contained"
                  size="small"
                  color="secondary"
                  onClick={e => this.props.openDeleteDialog(product)}>Delete</Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    );
  }
}

export default List;
