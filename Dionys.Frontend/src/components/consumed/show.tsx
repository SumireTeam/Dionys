import React from "react";
import { Table, TableBody, TableRow, TableCell } from "@material-ui/core";
import { IConsumed, formatDateTime } from "../../models";

interface Props {
    consumed: IConsumed;
}

const Show = ({ consumed }: Props) => {
    if (!consumed) {
        return null;
    }

    return (
        <Table>
            <TableBody>
                <TableRow>
                    <TableCell component="th" scope="row">
                        Product
                    </TableCell>
                    <TableCell>{consumed.product ? consumed.product.name : ""}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Weight
                    </TableCell>
                    <TableCell>{consumed.weight}</TableCell>
                </TableRow>

                <TableRow>
                    <TableCell component="th" scope="row">
                        Date
                    </TableCell>
                    <TableCell>{formatDateTime(consumed.date)}</TableCell>
                </TableRow>
            </TableBody>
        </Table>
    );
};

export default Show;
