export const SET_DESCRIPTION = 'SET_DESCRIPTION';


export const setDescription = (message) => {
  return {
    type: SET_DESCRIPTION,
    payload: message,
  };
};
