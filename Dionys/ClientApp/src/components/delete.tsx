import React from 'react';
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
} from '@material-ui/core';

interface Props {
  open: boolean;
  title: string;
  text: string;
  onDismiss: () => void;
  onConfirm: () => void;
}

class DeleteDialog extends React.Component<Props, {}> {
  public constructor(props) {
    super(props);

    this.state = {};
  }

  public render() {
    return (
      <Dialog open={this.props.open} onClose={e => this.props.onDismiss()}>
        <DialogTitle>{this.props.title}</DialogTitle>

        <DialogContent>
          <DialogContentText>{this.props.text}</DialogContentText>
        </DialogContent>

        <DialogActions>
          <Button onClick={e => this.props.onDismiss()} color="primary">No</Button>
          <Button onClick={e => this.props.onConfirm()} color="primary" autoFocus>Yes</Button>
        </DialogActions>
      </Dialog>
    );
  }
}

export default DeleteDialog;
