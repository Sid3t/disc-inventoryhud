import { createMuiTheme } from '@material-ui/core';
import { grey, red } from '@material-ui/core/colors';

const theme = createMuiTheme(
  {
    typography: {
      fontFamily: [
        '"Segoe UI"',
        'Roboto',
      ].join(','),
    },
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
          backgroundColor: 'rgba(44,47,51,0.1)',
        },
        elevation1: {
          backgroundColor: 'rgba(44,47,51,0.8)',
        },
        elevation24: {
          backgroundColor: 'rgba(44,47,51,0.8)',
        }
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
      MuiMenu: {
        paper: {
          backgroundColor: 'rgba(44,47,51,0.8)',
        },
      },
      MuiMenuItem: {
        selected: {
          backgroundColor: 'rgba(44,47,51,0.8)',
        },
      },
      MuiButton: {
        contained: {
          backgroundColor: 'rgba(44,47,51,0.4)',
        },
      },
    },
  },
);

export default theme;
