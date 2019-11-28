import React from "react";
import { Table, TableHead, TableBody, TableRow, TableCell, Button } from "@material-ui/core";
import { IConsumed, formatDateTime } from "../../models";
import LinkAdapter from "../link-adapter";

interface Props {
    consumed: IConsumed[];
    openDeleteDialog: (item: IConsumed) => void;
}

class List extends React.Component<Props, {}> {
    constructor(props: Props) {
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
                            <TableCell component="th" scope="row">
                                {consumed.product ? consumed.product.name : ""}
                            </TableCell>
                            <TableCell align="right">{consumed.weight}</TableCell>
                            <TableCell>{formatDateTime(consumed.date)}</TableCell>

                            <TableCell className="list-actions">
                                <Button
                                    className="button"
                                    variant="contained"
                                    size="small"
                                    color="primary"
                                    component={LinkAdapter}
                                    to={`/consumed/${consumed.id}`}
                                >
                                    More details
                                </Button>

                                <Button className="button" variant="contained" size="small" component={LinkAdapter} to={`/consumed/${consumed.id}/edit`}>
                                    Modify
                                </Button>

                                <Button
                                    className="button"
                                    variant="contained"
                                    size="small"
                                    color="secondary"
                                    onClick={() => this.props.openDeleteDialog(consumed)}
                                >
                                    Remove
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        );
    }
}

export default List;
