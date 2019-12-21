import IconButton from '@material-ui/core/IconButton';
import React from 'react';
import CancelIcon from '@material-ui/icons/Cancel';
import { connect } from 'react-redux';
import { hideApp } from '../../../containers/App/actions';
import Nui from '../../../util/Nui';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
  button: {
    bottom: 0,
    left: '50%',
    position: 'absolute',
    transform: 'translate(-100%, -50%)',
    zIndex: 999999,
  },
}));

export default connect()((props) => {
    const classes = useStyles();
    const handleClose = event => {
      props.dispatch(hideApp);
      Nui.send('CloseUI');
    };
    return (
      <IconButton onClick={handleClose} className={classes.button}>
        <CancelIcon/>
      </IconButton>
    );
  },
);
