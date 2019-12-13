import Grid from '@material-ui/core/Grid';
import { makeStyles, Typography } from '@material-ui/core';
import Inventory from '../Inventory';
import React from 'react';
import Hotbar from '../Hotbar/Hotbar';
import RightSideInfo from '../UserInfo/RightSideInfo';

const useStyles = makeStyles(theme => ({
  grid: {
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    margin: 'auto',
    position: 'absolute',
    width: '100%',
    height: '100%',
  },
  inventory: {
    width: '100%',
    height: '100%',
  },
  gridItem: {
    height: '70%',
    maxHeight: '70%',
  },
  gridItemOverflow: {
    height: '70%',
    maxHeight: '70%',
    overflow: 'visible',
  },
}));

export default (props) => {
  const classes = useStyles();

  return (
    <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.grid}>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12} className={classes.gridItem}>
            <Inventory/>
          </Grid>
          <Grid item xs={12}>
            <Hotbar/>
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12} className={classes.gridItem}>
            <Inventory/>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
