import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core';
import Inventory from '../Inventory';
import React from 'react';
import Hotbar from '../Hotbar/Hotbar';
import RightSideInfo from '../UserInfo/RightSideInfo';
import { useSelector } from 'react-redux';

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
    height: '100%'
  },
  gridItem: {
    height: '70%',
    maxHeight: '70%',
  }
}));

export default (props) => {
  const inventory = useSelector(state => state.inventory.player);
  const hotbar = useSelector(state => state.inventory.hotbar);
  const equipment = useSelector(state => state.inventory.equipment);
  const classes = useStyles();
  return (
    <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.grid}>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12} className={classes.gridItem}>
            <Inventory data={inventory}/>
          </Grid>
          <Grid item xs={12}>
            <Hotbar data={hotbar}/>
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={6} className={classes.inventory}>
        <Grid container justify={'flex-start'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
          <Grid item xs={12}>
            <Inventory slotCount={2} data={equipment}/>
          </Grid>
          <Grid item xs={12} className={classes.gridItem}>
            <RightSideInfo/>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
