import { makeStyles } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import React from 'react';
import DescriptionBox from '../DescriptionBox/DescriptionBox';

const useStyles = makeStyles(theme => ({
  inventory: {
    padding: theme.spacing(2),
    width: '100%',
    height: '100%',
    overflow: 'visible',
  },
  paper: {
    padding: theme.spacing(0),
    width: '100%',
    height: '100%',
  },
  gridItem: {
    height: '25vh',
    maxHeight: '25vh',
    overflow: 'auto',
  },
}));


export default (props) => {
  const classes = useStyles();
  return (
      <Grid container justify={'center'} alignItems={'center'} spacing={3} className={classes.inventory}>
        <Grid item xs={12} className={classes.gridItem}>
          <DescriptionBox />
        </Grid>
      </Grid>
  );
}
