import { useSelector } from 'react-redux';
import DoubleInventory from '../../Inventory/DoubleInventory/DoubleInventory';
import React from 'react';

export default (props) => {
  const inventory = useSelector(state=> state.inventory.vehicle);
  return <DoubleInventory inventory={inventory} />
}
