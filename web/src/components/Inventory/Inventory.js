import React from 'react';
import InventorySlot from './InventorySlot/InventorySlot';
import { range } from './../../util/list';
import { makeStyles } from '@material-ui/core';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles(theme => ({
  inventory: {
    width: '95%',
    maxHeight: '100%',
    height: '100%',
    margin: 0,
    overflowY: 'auto',
    overflowX: 'visible',
    padding: theme.spacing(2),
  },
}));

export default (props) => {
  const classes = useStyles();
  const slotCount = props.slotCount ? props.slotCount : 24;
  const data = props.data && props.data.Inventory ? props.data.Inventory : {};
  const type = props.data ? props.data['Type'] : '';
  const owner = props.data ? props.data.Owner : '';
  return (
    <Grid container justify={'center'} alignItems={'flex-start'} spacing={3} className={classes.inventory}>
      {range(1, slotCount).map(
        slot => <InventorySlot xs={props.itemXS ? props.itemXS : 2}
                               slot={slot}
                               owner={owner}
                               type={type}
                               item={data[slot.toString()]}
                               drawSlot={props.drawSlotNumber}
        />,
      )}
    </Grid>
  );
}
