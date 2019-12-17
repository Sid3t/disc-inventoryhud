import { SET_DESCRIPTION } from './actions';

export const initialState = {
  message: '',
};

const appReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_DESCRIPTION:
      return {
        ...state,
        message: action.payload,
      };
    default:
      return state;
  }
};

export default appReducer;
