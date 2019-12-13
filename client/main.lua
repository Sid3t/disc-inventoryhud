Citizen.CreateThread(function()
    while true do
        Citizen.Wait(0)
        if IsControlJustReleased(0, Config.OpenInventoryKey) then
            SendNUIMessage({
                type = "APP_SHOW"
            })
            SetNuiFocus(true, true)
        end
    end
end)

RegisterNUICallback('CloseUi', function(data, cb)
    SetNuiFocus(false, false)
end)

AddEventHandler("onResourceStop", function()
    SetNuiFocus(false, false)
end)