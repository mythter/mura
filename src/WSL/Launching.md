# Launching

````powershell
wsl                                 # Launch default distro
wsl -d <Distro>                     # Launch a specific distro
wsl -d <Distro> -u <user>           # Launch as a specific user
````

## Examples

````powershell
wsl -d Ubuntu -e ls -la
wsl -d Ubuntu -u root
````
