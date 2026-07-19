# Managing Distros

```powershell
wsl --list --verbose               # List installed distros, version, state (short: wsl -l -v)
wsl --list --online                # List distros available to install (short: wsl -l -o)
wsl --list --running               # List currently running distros
wsl --set-default <Distro>         # Set default distro
wsl --set-version <Distro> 2       # Convert a distro to WSL2 (use 1 for WSL1)
wsl --terminate <Distro>           # Stop a specific distro
wsl --shutdown                     # Stop the WSL VM and all distros
wsl --unregister <Distro>          # Remove a distro completely (deletes its data!)
wsl --export <Distro> <file.tar>   # Backup a distro to a tar file
wsl --import <Distro> <InstallLoc> <file.tar>  # Restore/import a distro from tar
```

## Notes

- `wsl --unregister` is irreversible – export a backup first if you want to keep the data
- `wsl --export` / `--import` is also a handy way to clone or move a distro to another machine
- `wsl --shutdown` fully stops the WSL2 VM (all distros), useful after changing `.wslconfig`
