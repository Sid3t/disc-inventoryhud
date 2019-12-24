import { APP_HIDE, APP_SHOW, HOTBAR_HIDE, HOTBAR_SHOW } from './actions';

export const initialState = {
  hidden: true,
  hotbar: false
};

const appReducer = (state = initialState, action) => {
  switch (action.type) {
    case APP_SHOW:
      return {
        ...state,
        hidden: false,
      };
    case APP_HIDE:
      return {
        ...state,
        hidden: true,
      };
    case HOTBAR_SHOW:
      return {
        ...state,
        hotbar: true,
      };
    case HOTBAR_HIDE:
      return {
        ...state,
        hotbar: false,
      };
    default:
      return state;
  }
};

export default appReducer;
