import React from "react";
import { TextField, Button } from "@material-ui/core";
import { Consumed } from "../../models";
import { ServiceProvider } from "../../services";
import Select from "../select";
import { getDate, setDate, getTime, setTime } from "../../models";

interface Props {
    consumed: Consumed;
    onModelChange: (consumed: Consumed) => void;
    onSubmit: () => void;
}

interface State {
    readonly nameTouched: boolean;
}

interface Option {
    readonly value: string;
    readonly label: string;
}

const search = async (name: string, callback: (options: Option[]) => void) => {
    const service = ServiceProvider.productService;
    const products = await service.search(name);
    const options = products.map(product => ({
        value: product.id,
        label: product.name
    }));

    callback(options);
};

const numRegExp = new RegExp(/^\d+(\.\d+)?$/);

class Edit extends React.Component<Props, State> {
    public constructor(props) {
        super(props);
    }

    public render() {
        if (!this.props.consumed) {
            return null;
        }

        const onChange = this.props.onModelChange;

        const defaultProduct = this.props.consumed.product
            ? {
                  label: this.props.consumed.product.name,
                  value: this.props.consumed.product.id
              }
            : {
                  label: "",
                  value: ""
              };

        return (
            <form className="form" noValidate autoComplete="off">
                <Select
                    className="field"
                    label="Product"
                    cacheOptions
                    loadOptions={search}
                    defaultValue={defaultProduct}
                    onChange={(e: any) => onChange({ ...this.props.consumed, productId: e.value })}
                />

                <TextField
                    className="field"
                    variant="outlined"
                    label="Weight"
                    value={this.props.consumed.weight}
                    onChange={e => numRegExp.test(e.target.value) && onChange({ ...this.props.consumed, weight: +e.target.value })}
                    type="number"
                    inputProps={{ min: "0" }}
                    fullWidth
                    required
                />

                <div>
                    <TextField
                        className="field"
                        variant="outlined"
                        label="Date"
                        type="date"
                        value={getDate(this.props.consumed.date)}
                        onChange={e =>
                            onChange({
                                ...this.props.consumed,
                                date: setDate(this.props.consumed.date, e.target.value)
                            })
                        }
                        required
                    />

                    <TextField
                        className="field"
                        variant="outlined"
                        label="Time"
                        type="time"
                        value={getTime(this.props.consumed.date)}
                        onChange={e =>
                            onChange({
                                ...this.props.consumed,
                                date: setTime(this.props.consumed.date, e.target.value)
                            })
                        }
                        required
                    />
                </div>

                <Button className="button" variant="contained" color="primary" onClick={this.props.onSubmit}>
                    {this.props.consumed.id ? "Update" : "Create"}
                </Button>
            </form>
        );
    }
}

export default Edit;
