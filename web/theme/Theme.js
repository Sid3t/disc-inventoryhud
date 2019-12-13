import { createMuiTheme } from '@material-ui/core';
import { grey, red } from '@material-ui/core/colors';

const theme = createMuiTheme(
  {
    typography: {},
    palette: {
      type: 'dark',
      primary: {
        main: '#b71c1c',
      },
      secondary: {
        main: grey[400],
      },
      error: red,
    },
    overrides: {
      MuiPaper: {
        root: {
          backgroundColor: 'rgba(44,47,51,0.6)',
        },
      },
      MuiDivider: {
        root: {
          backgroundColor: '#b71c1c',
        },
      },
      MuiFab: {
        root: {
          backgroundColor: '#2c2f33',
        },
      },
    },
  },
);

export default theme;
