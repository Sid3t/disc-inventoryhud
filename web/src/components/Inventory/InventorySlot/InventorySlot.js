import Grid from '@material-ui/core/Grid';
import React from 'react';
import { makeStyles, Paper } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
  slot: {
    width: '100%',
    height: 120
  }
}));


export default (props) => {
  const classes = useStyles();
  return (
    <Grid item xs={props.xs}>
      <Paper className={classes.slot}>
        This is a slot
      </Paper>
    </Grid>
  )
}
