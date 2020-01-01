import Paper from '@material-ui/core/Paper';
import React from 'react';
import { makeStyles } from '@material-ui/core';
import { useSelector } from 'react-redux';
import Table from '@material-ui/core/Table';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import lodash from 'lodash'

const useStyles = makeStyles(theme => ({
  paper: {
    width: '100%',
    height: '100%',
    backgroundColor: 'rgba(44,47,51,0.8)',
  }
}));

export default (props) => {
  const description = useSelector(state => state.description);
  const classes = useStyles();
  return (
    <Paper className={classes.paper}>
      <Table className={classes.table} size="small" aria-label="a dense table">
        <TableBody>
          {lodash.map(description.message, (value, key) => {
            return <TableRow><TableCell>{key}</TableCell><TableCell>{value}</TableCell></TableRow>
          })}
        </TableBody>
      </Table>
    </Paper>
  );
}
