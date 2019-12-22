import Paper from '@material-ui/core/Paper';
import React from 'react';
import { makeStyles } from '@material-ui/core';
import { useSelector } from 'react-redux';

const useStyles = makeStyles(theme => ({
  paper: {
    padding: theme.spacing(0),
    width: '100%',
    height: '100%',
    backgroundColor: 'rgba(44,47,51,0.8)',
  },
}));

export default (props) => {
  const description = useSelector(state => state.description);
  const classes = useStyles();
  return (
    <Paper className={classes.paper}>
      {JSON.stringify(description.message, 2, null)}
    </Paper>
  );
}
