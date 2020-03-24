# WiFi Password

Get all WiFi SSID's

```cmd
netsh wlan show profile
```

Show password of one SSID
```cmd
netsh wlan show profile "MyWiFiName" key=clear
```

See entry "Security settings", "Key Content"