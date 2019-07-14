import React from 'react';
import {
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
} from '@material-ui/core';
import { Link } from '../../components';
import { Product } from '../../services';

interface Props {
  products: Product[];
}

const List = ({ products }: Props) => {
  return (
    <Table>
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
        {products.map(product => (
          <TableRow key={product.id}>
            <TableCell component="th" scope="row">{product.name}</TableCell>
            <TableCell align="right">{product.proteins}</TableCell>
            <TableCell align="right">{product.fats}</TableCell>
            <TableCell align="right">{product.carbohydrates}</TableCell>
            <TableCell align="right">{product.energy}</TableCell>

            <TableCell>
              <Link href="/products/[id]" as={`/products/${product.id}`}>View</Link>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};

export default List;
