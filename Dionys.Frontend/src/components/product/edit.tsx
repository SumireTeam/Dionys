import React from "react";
import { TextField, Button } from "@material-ui/core";
import { Product } from "../../models";

interface Props {
    product: Product;
    onModelChange: (product: Product) => void;
    onSubmit: () => void;
}

interface State {
    readonly nameTouched: boolean;
}

const numRegExp = new RegExp(/^\d+(\.\d+)?$/);

class Edit extends React.Component<Props, State> {
    public constructor(props) {
        super(props);

        this.state = {
            nameTouched: false
        };
    }

    public render() {
        if (!this.props.product) {
            return null;
        }

        const onChange = this.props.onModelChange;

        return (
            <form className="form" noValidate autoComplete="off">
                <TextField
                    className="field"
                    variant="outlined"
                    label="Name"
                    value={this.props.product.name}
                    onBlur={() => this.setState({ nameTouched: true })}
                    onChange={e => onChange({ ...this.props.product, name: e.target.value })}
                    error={this.state.nameTouched && !this.props.product.name.length}
                    fullWidth
                    required
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Description"
                    value={this.props.product.description}
                    onChange={e => onChange({ ...this.props.product, description: e.target.value })}
                    fullWidth
                    multiline
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Protein"
                    value={this.props.product.protein}
                    onChange={e => numRegExp.test(e.target.value) && onChange({ ...this.props.product, protein: +e.target.value })}
                    type="number"
                    inputProps={{ min: "0" }}
                    fullWidth
                    required
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Fat"
                    value={this.props.product.fat}
                    onChange={e => numRegExp.test(e.target.value) && onChange({ ...this.props.product, fat: +e.target.value })}
                    type="number"
                    inputProps={{ min: "0" }}
                    fullWidth
                    required
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Carbs"
                    value={this.props.product.carbs}
                    onChange={e => numRegExp.test(e.target.value) && onChange({ ...this.props.product, carbs: +e.target.value })}
                    type="number"
                    inputProps={{ min: "0" }}
                    fullWidth
                    required
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Calories"
                    value={this.props.product.calories}
                    onChange={e => numRegExp.test(e.target.value) && onChange({ ...this.props.product, calories: +e.target.value })}
                    type="number"
                    inputProps={{ min: "0" }}
                    fullWidth
                    required
                />

                <Button className="button" variant="contained" color="primary" onClick={this.props.onSubmit}>
                    {this.props.product.id ? "Update" : "Create"}
                </Button>
            </form>
        );
    }
}

export default Edit;
