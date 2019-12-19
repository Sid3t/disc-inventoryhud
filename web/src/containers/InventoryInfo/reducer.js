import { SET_INFO } from './actions';

export const initialState = {
  info: {},
};

const appReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_INFO:
      return {
        ...state,
        info: action.payload.data,
      };
    default:
      return state;
  }
};

export default appReducer;
