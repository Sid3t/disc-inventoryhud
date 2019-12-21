import { useSelector } from 'react-redux';
import DoubleInventory from '../../Inventory/DoubleInventory/DoubleInventory';
import React from 'react';

export default (props) => {
  const inventory = useSelector(state=> state.inventory.glovebox);
  return <DoubleInventory inventory={inventory} />
}
