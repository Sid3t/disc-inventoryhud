import React from 'react';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { makeStyles, MuiThemeProvider } from '@material-ui/core';
import Theme from '../../../theme/Theme';
import Screen from '../../components/UI/AppScreen/AppScreen';

const useStyles = makeStyles(theme => ({}));


const App = ({ hidden }) => {
  const classes = useStyles();


  return (
    <MuiThemeProvider theme={Theme}>
      <Screen hidden={hidden}>
      </Screen>
    </MuiThemeProvider>

  );
};

App.propTypes = {
  hidden: PropTypes.bool.isRequired,
};

const mapStateToProps = state => ({ hidden: state.app.hidden });

export default connect(mapStateToProps)(App);
