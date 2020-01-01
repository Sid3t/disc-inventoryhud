import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { makeStyles, MuiThemeProvider } from '@material-ui/core';
import Theme from '../../../theme/Theme';
import Screen from '../../components/UI/AppScreen/AppScreen';
import { hideApp } from './actions';
import Nui from '../../util/Nui';

const useStyles = makeStyles(theme => ({}));


const App = (props) => {
  const classes = useStyles();
  const closeFunction = (event) => {
    if(event.which === 27 || event.which === 113) {
      props.dispatch(hideApp);
      Nui.send('CloseUI');
    }
  };
  useEffect(() => {
    window.addEventListener('keydown', closeFunction);
    return () => window.removeEventListener('keydown', closeFunction);
  }, []);

  return (
    <MuiThemeProvider theme={Theme}>
      <Screen hidden={props.hidden} />
    </MuiThemeProvider>

  );
};

App.propTypes = {
  hidden: PropTypes.bool.isRequired,
};

const mapStateToProps = state => ({ hidden: state.app.hidden });

export default connect(mapStateToProps)(App);
