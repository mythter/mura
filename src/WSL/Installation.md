# Installation and Setup

Run these from Windows PowerShell (as Administrator for the first install).

````powershell
wsl --install                      # Install WSL + default Ubuntu distro
wsl --install -d <Distro>          # Install a specific distro (e.g. Debian, Kali-Linux)
wsl --list --online                # List distros available to install
wsl --set-default-version 2        # Set WSL2 as default for new installs
wsl --version                      # Show WSL component versions
````

## Notes

* A system restart is usually required after the first `wsl --install`
