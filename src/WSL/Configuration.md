# Configuration

## `.wslconfig` – global settings

Location: `C:\Users\<YourName>\.wslconfig`

````ini
[wsl2]
memory=4GB
processors=2
swap=2GB
localhostForwarding=true
````

## `wsl.conf` – per-distro settings

Location (inside distro): `/etc/wsl.conf`

````ini
[boot]
systemd=true

[automount]
enabled=true
options="metadata,umask=22,fmask=11"

[network]
generateResolvConf=true
````

## Apply changes

Changes to either file require a full VM restart to take effect:

````powershell
wsl --shutdown
````
