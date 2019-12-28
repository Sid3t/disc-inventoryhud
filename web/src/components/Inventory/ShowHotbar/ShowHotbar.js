import Hotbar from '../Hotbar/Hotbar';
import React from 'react';
import { useSelector } from 'react-redux';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
  grid: {
    bottom: 0,
    position: 'absolute',
    width: '100%',
  },
}));

export default (props) => {
  const classes = useStyles();
  const hotbar = useSelector(state => state.inventory.hotbar);
  const show = useSelector(state => state.app.hotbar);
  return (
    <Grid className={classes.grid} style={{ visibility: show ? 'visible' : 'hidden' }} container>
      <Grid item xs={3}/>
      <Grid item xs={6}>
        <Hotbar data={hotbar}/>
      </Grid>
      <Grid item xs={3}/>
    </Grid>
  );
}
