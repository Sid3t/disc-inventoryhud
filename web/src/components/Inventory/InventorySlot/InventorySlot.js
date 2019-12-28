import Grid from '@material-ui/core/Grid';
import React, { Fragment } from 'react';
import { makeStyles, Menu, Paper } from '@material-ui/core';
import MenuItem from '@material-ui/core/MenuItem';
import Img from 'react-image';
import CircularProgress from '@material-ui/core/CircularProgress';
import Typography from '@material-ui/core/Typography';
import { connect, useSelector } from 'react-redux';
import { setDescription } from '../DescriptionBox/actions';
import { dropItem, moveItem, setHoverItem, useItem } from '../../UI/AppScreen/actions';
import CountSelector from '../../UI/CountSelector/CountSelector';

const useStyles = makeStyles(theme => ({
  slot: {
    width: '100%',
    height: 120,
    padding: theme.spacing(1),
    position: 'relative',
    textAlign: 'center',
    userSelect: 'none',
    backgroundColor: 'rgba(44,47,51,0.8)',
  },
  menu: {
    padding: theme.spacing(2),
    width: '5vw',
  },
  img: {
    width: '100%',
    height: '80%',
    userSelect: 'none',
  },
  name: {
    bottom: 0,
    left: '50%',
    height: '20%',
    width: '100%',
    position: 'absolute',
    transform: 'translate(-50%, 0%)',
    backgroundColor: 'rgba(44,47,51,0.1)',
  },
  paper: {
    backgroundColor: 'rgba(44,47,51,0.1)',
  },
  countGrid: {
    position: 'absolute',
    right: 0,
    top: 0,
    height: '20%',
    userSelect: 'none',
  },
  slotNumberGrid: {
    position: 'absolute',
    left: 0,
    top: 0,
    width: '20%',
    height: '20%',
    userSelect: 'none',
  },
}));
const initialState = {
  mouseX: null,
  mouseY: null,
};


export default connect()((props) => {
  const classes = useStyles();
  const [state, setState] = React.useState(initialState);
  const [countState, setCountState] = React.useState(false);
  const [dropCountState, setDropCountState] = React.useState(false);
  const hoverItem = useSelector(state => state.inventory.hoverItem);
  const iD = useSelector(state => state.itemData.info);

  const itemData = () => {
    return iD[props.item.Id] ? iD[props.item.Id] : {};
  };

  const handleClick = event => {
    event.preventDefault();
    setState({
      mouseX: event.clientX - 2,
      mouseY: event.clientY - 4,
    });
  };
  const handleClose = (event) => {
    switch (event.target.id) {
      case 'split': {
        if (props.item.Count > 2) {
          setCountState(true);
        } else {
          const item = { ...props.item };
          item.Count = 1;
          props.dispatch(setHoverItem(props.slot, item, props.type, props.owner));
        }
        break;
      }
      case 'drop': {
        if (props.item.Count > 1) {
          setDropCountState(true);
        } else {
          props.dispatch(dropItem(props.slot, props.type, props.item, props.owner));
        }
        break;
      }
      case 'use': {
        useItem(props.slot, props.type, props.item, props.owner)
      }
    }
    setState(initialState);
  };

  const handleDrag = (slot, item) => {
    if (item) {
      props.dispatch(setHoverItem(slot, item, props.type, props.owner));
    }
  };

  const handleMouseDown = (event) => {
    if (countState || state.mouseY !== null || state.mouseX !== null || hoverItem || dropCountState) return;
    if (event.button === 0) {
      if (props.item !== undefined) {
        handleDrag(props.slot, props.item);
      } else if (hoverItem != null) {
        props.dispatch(moveItem(props.slot, props.type, hoverItem, props.owner));
      }
    }
  };

  const handleMouseUp = (event) => {
    if (countState) return;
    if (hoverItem != null) {
      props.dispatch(moveItem(props.slot, props.type, hoverItem, props.owner));
    }
  };

  const split = (count) => {
    if (hoverItem != null) {
      const item = { ...hoverItem };
      item.data.item.Count = count;
      props.dispatch(moveItem(props.slot, props.type, props.owner));
    } else {
      const item = { ...props.item };
      item.Count = count;
      props.dispatch(setHoverItem(props.slot, item, props.type, props.owner));
    }
  };

  const drop = count => {
    const item = { ...props.item };
    item.Count = count;
    props.dispatch(dropItem(props.slot, props.type, item, props.owner));
  };
  return (
    <Grid item xs={props.xs} onMouseDown={handleMouseDown}
          onMouseUp={handleMouseUp}
          onContextMenu={handleClick}
          onMouseEnter={() => props.dispatch(setDescription(props.item ? props.item.MetaData : ''))}
          onMouseLeave={() => props.dispatch(setDescription(''))}>
      <Paper className={classes.slot}>
        {props.item && <Fragment>
          <Img className={classes.img}
               src={itemData().ItemUrl}
               loader={<CircularProgress/>}
               unloader={<Typography variant={'body2'}>{props.item.id}</Typography>}/>
          <Grid className={classes.countGrid} spacing={1} container justify={'center'} alignItems={'center'}>
            <Grid item xs={12}>
              <Paper className={classes.paper}>{props.item.Count}</Paper>
            </Grid>
          </Grid>
          <Paper className={classes.name}>{itemData().Label}</Paper>
        </Fragment>
        }
        {props.drawSlot &&
        <Grid spacing={1} className={classes.slotNumberGrid} container justify={'center'} alignItems={'center'}>
          <Grid item xs={12}>
            <Paper className={classes.paper}>{props.slot}</Paper>
          </Grid>
        </Grid>}
      </Paper>
      {props.item && <Menu
        className={classes.menu}
        keepMounted
        open={state.mouseY !== null}
        onClose={handleClose}
        anchorReference="anchorPosition"
        anchorPosition={
          state.mouseY !== null && state.mouseX !== null
            ? { top: state.mouseY, left: state.mouseX }
            : undefined
        }
      >
        {!props.hideUse && <MenuItem onClick={handleClose} id={'use'}>
          Use
        </MenuItem> }
        {!props.hideGive && <MenuItem onClick={handleClose}>
          Give
        </MenuItem> }
        {!props.hideDrop && <MenuItem onClick={handleClose} id={'drop'}>
          Drop
        </MenuItem> }
        {props.item.Count > 1 && <MenuItem onClick={handleClose} id={'split'}>
          Split
        </MenuItem>}
      </Menu>}
      {countState && <CountSelector open={countState} setDialogState={setCountState}
                                    maxCount={hoverItem != null ? hoverItem.data.item.Count - 1 : props.item.Count - 1}
                                    action={split} actionName={"Split"}/>}
      {dropCountState && <CountSelector open={dropCountState} setDialogState={setDropCountState}
                                        maxCount={props.item.Count}
                                        action={drop} actionName={"Drop"}/>}
    </Grid>);
});
