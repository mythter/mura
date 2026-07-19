# Networking

## From inside WSL

````bash
ip addr show eth0                   # Get WSL instance's IP address
cat /etc/resolv.conf                # Check DNS config
````

## From Windows PowerShell

````powershell
wsl hostname -I                     # Get WSL IP from Windows side
````

## Notes

* WSL2 uses a virtual network adapter; the IP changes on each restart unless configured otherwise
* `localhostForwarding=true` in `.wslconfig` lets you reach WSL services via `localhost` from Windows (enabled by default in most setups)
