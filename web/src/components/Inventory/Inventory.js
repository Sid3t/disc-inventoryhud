import React from 'react';
import InventorySlot from './InventorySlot/InventorySlot';
import { range } from './../../util/list';
import { makeStyles } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles(theme => ({
  inventory: {
    padding: theme.spacing(2),
    width: '100%',
    maxHeight: '100%',
    height: '100%',
  },
  paper: {
    maxHeight: '100%',
    height: '100%',
    padding: theme.spacing(1),
    overflow: 'auto',
  },
}));

export default (props) => {
  const classes = useStyles();

  const slotCount = props.slotCount ? props.slotCount : 48;

  const itemXS = 12 / props.slotCount;

  return (
    <Paper className={classes.paper}>
      <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={1} className={classes.inventory}>
        {range(1, slotCount).map(
          slot => <InventorySlot xs={2}/>,
        )}
      </Grid>
    </Paper>
  );
}
