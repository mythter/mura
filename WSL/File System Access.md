# File System Access

## From inside WSL → Windows drives

```bash
/mnt/c/Users/<YourName>/            # C: drive
/mnt/d/                             # D: drive
```

```powershell
explorer.exe .                      # Open current WSL folder in File Explorer (run inside WSL)
```

## From Windows → WSL files

```
\\wsl$\<Distro>\home\<user>\        # File Explorer path
```
