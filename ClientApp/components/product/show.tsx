import React from 'react';
import {
  Table,
  TableBody,
  TableRow,
  TableCell,
} from '@material-ui/core';
import { Product } from '../../services';

interface Props {
  product: Product;
}

const Show = ({ product }: Props) => {
  return (
    <Table>
      <TableBody>
        <TableRow>
          <TableCell component="th" scope="row">Name</TableCell>
          <TableCell>{product.name}</TableCell>
        </TableRow>

        <TableRow>
          <TableCell component="th" scope="row">Protein</TableCell>
          <TableCell>{product.proteins}</TableCell>
        </TableRow>

        <TableRow>
          <TableCell component="th" scope="row">Fat</TableCell>
          <TableCell>{product.fats}</TableCell>
        </TableRow>

        <TableRow>
          <TableCell component="th" scope="row">Carbs</TableCell>
          <TableCell>{product.carbohydrates}</TableCell>
        </TableRow>

        <TableRow>
          <TableCell component="th" scope="row">Calories</TableCell>
          <TableCell>{product.energy}</TableCell>
        </TableRow>
      </TableBody>
    </Table>
  );
};

export default Show;
