# [ESX] [DISC] disc-inventoryhud
The ACTUAL real code for the inventoryhud that is not broken

# ESX Compatibility

This is a fork for use with the inventory [Rex's ESX](https://github.com/rex2630/es_extended)

# Events

## Add Item Event

```lua
TriggerEvent('disc-inventoryhud:addItem', self.source, name, count)
```

## Remove Item Event

```lua
TriggerEvent('disc-inventoryhud:removeItem', source, item.Id, 1, item.Slot, item.Inventory)
```

## Register Item for Use

```lua
TriggerEvent('disc-inventoryhud:registerItemUse', item, cb)
```

# BETA

This version contains everything besides shops and weapons. 

# Stream

I stream on [Twitch](https://www.twitch.tv/DiscworldZA). Come hang out and learn from me!

# Other Downloads

Other Downloads available on my [repo](https://github.com/DiscworldZA/gta-resources)
