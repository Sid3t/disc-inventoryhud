import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import React from 'react';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
  slot: {
    width: '100%',
    height: 120,
  }, paper: {
    padding: theme.spacing(1),
  },
}));

export default (props) => {
  const classes = useStyles();
  return (
    <Grid container alignItems={'stretch'}>
      <Grid item xs={12}>
        <Paper className={classes.paper}>
          <Paper className={classes.slot}>
            This is a slot
          </Paper>
        </Paper>
      </Grid>
    </Grid>
  );
}
