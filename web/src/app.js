import '@babel/polyfill';

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import App from 'containers/App';

import WindowListener from 'containers/WindowListener';

import configureStore from './configureStore';
import Nui from './util/Nui';
import { hideApp } from './containers/App/actions';
import SingleInventory from './components/Inventory/SingleInventory/SingleInventory';
import HoverItem from './components/Inventory/HoverItem/HoverItem';
import Paper from '@material-ui/core/Paper';

const initialState = {};
const store = configureStore(initialState);
const MOUNT_NODE = document.getElementById('app');

const closeUI = (event) => {
  if (event.keyCode === 27) {
    Nui.send('CloseUI');
    props.dispatch(hideApp);
  }
};



const render = () => {

  ReactDOM.render(
    <Provider store={store}>
      <WindowListener>
        <App/>
      </WindowListener>
    </Provider>,
    MOUNT_NODE,
  );
};

if (module.hot) {
  module.hot.accept(['containers/App'], () => {
    ReactDOM.unmountComponentAtNode(MOUNT_NODE);
    render();
  });
}

render();
