import Dialog from '@material-ui/core/Dialog';
import React, { useState } from 'react';
import { DialogContent, TextField } from '@material-ui/core';
import Slider from '@material-ui/core/Slider';
import Typography from '@material-ui/core/Typography';
import DialogActions from '@material-ui/core/DialogActions';
import Button from '@material-ui/core/Button';

export default (props) => {
  const [value, setValue] = useState(1);
  const setCombinedValue = value => {
    if (value > props.maxCount) {
      setValue(props.maxCount);
    } else if (value <= 0) {
      setValue(1);
    } else {
      setValue(value);
    }
  };

  return (
    <Dialog
      fullWidth
      maxWidth={'sm'}
      open={props.open}
      onClose={() => props.setDialogState(false)}
    >
      <DialogContent>
        <Typography id="discrete-slider-always" gutterBottom>
          {props.message ? props.message : 'Count:'}
        </Typography>
        <Slider
          max={props.maxCount}
          defaultValue={1}
          step={1}
          value={value}
          onChange={(event, value) => setCombinedValue(value)}
          valueLabelDisplay="on"
        />
        <TextField variant={'outlined'} value={value} onChange={(event) => {
          setCombinedValue(event.target.value);
        }
        } label={props.message ? props.message : 'Count'} type={'number'}/>
      </DialogContent>
      <DialogActions>
        <Button variant={'contained'} onClick={() => props.setDialogState(false)}>Cancel</Button>
        <Button variant={'contained'} onClick={() => {
          props.setDialogState(false);
          props.action(value);
        }}>{props.actionName ? props.actionName : "Split"}</Button>
      </DialogActions>
    </Dialog>
  )
    ;
}
