resource_manifest_version '05cfa83c-a124-4cfa-a768-c24a5811d8f9'

description 'Disc InventoryHud Redo'

version '0.0.1'

client_scripts {
    '@es_extended/locale.lua',
    'client/main.lua',
    'config.lua',
}

server_scripts {
    '@es_extended/locale.lua',
    'server/main.lua',
    'config.lua',
}

ui_page "web/html/index.html"

files { "web/html/main.js", "web/html/index.html" }
