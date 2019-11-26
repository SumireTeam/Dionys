import React from "react";
import { Table, TableBody, TableRow, TableCell } from "@material-ui/core";
import { Product } from "../../models";

interface Props {
    product: Product;
}

const Show = ({ product }: Props) => {
    if (!product) {
        return null;
    }

    return (
        <Table>
            <TableBody>
                <TableRow>
                    <TableCell component="th" scope="row">
                        Name
                    </TableCell>
                    <TableCell>{product.name}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Description
                    </TableCell>
                    <TableCell>{product.description}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Protein
                    </TableCell>
                    <TableCell>{product.protein}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Fat
                    </TableCell>
                    <TableCell>{product.fat}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Carbs
                    </TableCell>
                    <TableCell>{product.carbs}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Calories
                    </TableCell>
                    <TableCell>{product.calories}</TableCell>
                </TableRow>
            </TableBody>
        </Table>
    );
};

export default Show;
