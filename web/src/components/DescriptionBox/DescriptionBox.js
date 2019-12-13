import Paper from '@material-ui/core/Paper';
import React from 'react';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
  paper: {
    padding: theme.spacing(0),
    width: '100%',
    height: '100%',
  },
}));

export default (props) => {
  const classes = useStyles();
  return (
    <Paper className={classes.paper}>
      This is my item description
    </Paper>
  );
}
