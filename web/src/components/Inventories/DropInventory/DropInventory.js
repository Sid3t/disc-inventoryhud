import { useSelector } from 'react-redux';
import DoubleInventory from '../../Inventory/DoubleInventory/DoubleInventory';
import React from 'react';

export default (props) => {
  const dropInventory = useSelector(state=> state.inventory.drop);
  return <DoubleInventory inventory={dropInventory} />
}
