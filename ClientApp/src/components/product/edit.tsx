import React from 'react';
import { TextField, Button } from '@material-ui/core';
import { Product } from '../../services';

interface Props {
  product: Product;
  onModelChange: (product: Product) => void;
  onSubmit: () => void;
}

const numRegExp = new RegExp(/^\d+(\.\d+)?$/);

const Edit = ({ product, onModelChange, onSubmit }: Props) => {
  if (!product) {
    return null;
  }

  return (
    <form className="form" noValidate autoComplete="off">
      <TextField className="field"
        variant="outlined"
        label="Name"
        value={product.name}
        onChange={e => onModelChange({ ...product, name: e.target.value })}
        error={!product.name.length}
        fullWidth
        required />

      <TextField className="field"
        variant="outlined"
        label="Description"
        value={product.commentary}
        onChange={e => onModelChange({ ...product, commentary: e.target.value })}
        fullWidth
        multiline />

      <TextField className="field"
        variant="outlined"
        label="Protein"
        value={product.proteins}
        onChange={e => numRegExp.test(e.target.value) && onModelChange({ ...product, proteins: +e.target.value })}
        type="number"
        inputProps={{ min: "0" }}
        fullWidth
        required />

      <TextField className="field"
        variant="outlined"
        label="Fat"
        value={product.fats}
        onChange={e => numRegExp.test(e.target.value) && onModelChange({ ...product, fats: +e.target.value })}
        type="number"
        inputProps={{ min: "0" }}
        fullWidth
        required />

      <TextField className="field"
        variant="outlined"
        label="Carbs"
        value={product.carbohydrates}
        onChange={e => numRegExp.test(e.target.value) && onModelChange({ ...product, carbohydrates: +e.target.value })}
        type="number"
        inputProps={{ min: "0" }}
        fullWidth
        required />

      <TextField className="field"
        variant="outlined"
        label="Calories"
        value={product.energy}
        onChange={e => numRegExp.test(e.target.value) && onModelChange({ ...product, energy: +e.target.value })}
        type="number"
        inputProps={{ min: "0" }}
        fullWidth
        required />

      <Button className="button"
        variant="contained"
        color="primary"
        onClick={onSubmit}>
        {product.id ? 'Update' : 'Create'}
      </Button>
    </form>
  );
};

export default Edit;
