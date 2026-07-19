# Useful Diagnostics

## From Windows

```powershell
wsl --status                        # Show default distro/version and WSL config
wsl --update                        # Update the WSL kernel
wsl --update --rollback             # Roll back kernel update
```

## From inside WSL

```bash
lsb_release -a                      # Distro version info
uname -a                            # Kernel info from inside WSL
cat /proc/version                   # Kernel build info
```
