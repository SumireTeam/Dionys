import React from 'react';
import { TextField, Button } from '@material-ui/core';
import { Consumed } from '../../models';

interface Props {
  consumed: Consumed;
  onModelChange: (consumed: Consumed) => void;
  onSubmit: () => void;
}

interface State {
  readonly nameTouched: boolean;
}

const numRegExp = new RegExp(/^\d+(\.\d+)?$/);

class Edit extends React.Component<Props, State> {
  public constructor(props) {
    super(props);
  };

  public render() {
    if (!this.props.consumed) {
      return null;
    }

    const onChange = this.props.onModelChange;

    return (
      <form className="form" noValidate autoComplete="off">
        <TextField className="field"
          variant="outlined"
          label="Product"
          value={this.props.consumed.productId}
          onChange={e => onChange({ ...this.props.consumed, productId: e.target.value })}
          fullWidth
          required />

        <TextField className="field"
          variant="outlined"
          label="Weight"
          value={this.props.consumed.weight}
          onChange={e => numRegExp.test(e.target.value)
            && onChange({ ...this.props.consumed, weight: +e.target.value })}
          type="number"
          inputProps={{ min: "0" }}
          fullWidth
          required />

        <TextField className="field"
          variant="outlined"
          label="Date"
          value={this.props.consumed.date.toISOString()}
          onChange={e => onChange({ ...this.props.consumed, date: new Date(e.target.value) })}
          fullWidth
          required />

        <Button className="button"
          variant="contained"
          color="primary"
          onClick={this.props.onSubmit}>
          {this.props.consumed.id ? 'Update' : 'Create'}
        </Button>
      </form>
    );
  }
}

export default Edit;
