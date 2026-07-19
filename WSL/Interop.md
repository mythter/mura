# Interop (Windows ↔ Linux)

## Run Linux commands from Windows

```powershell
wsl ls -la
wsl cat /etc/os-release
```

## Run Windows executables from WSL

```bash
notepad.exe file.txt
code .                              # Open current folder in VS Code (with WSL extension)
cmd.exe /c dir
```

## Pipe between Windows and Linux tools

```bash
ls | grep.exe "txt"
```

## Notes

- Install the **WSL** extension in VS Code, then run `code .` from inside your WSL distro to open the folder in "WSL: <Distro>" remote mode
- Windows `.exe` tools are available in WSL's `$PATH` by default (via `/mnt/c/...`), which is what makes `notepad.exe`, `code`, etc. work directly
