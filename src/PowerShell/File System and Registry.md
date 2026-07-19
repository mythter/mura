# File System & Registry

PowerShell drives (`PSDrive`) give a uniform way to browse the file
system, registry, certificates, and more using the same cmdlets.

## Navigating

````powershell
Get-Location                       # pwd
Set-Location C:\Temp               # cd
Push-Location C:\Temp; Pop-Location # Save/restore location
Get-PSDrive                        # List all drives (C:, HKLM:, Env:, etc.)
````

## Listing & Testing

````powershell
Get-ChildItem                       # ls / dir
Get-ChildItem -Recurse
Get-ChildItem -Filter *.log
Get-ChildItem -Include *.txt,*.csv -Recurse
Get-ChildItem -Force               # Include hidden/system items
Get-ChildItem -Directory            # Folders only
Get-ChildItem -File                 # Files only
Test-Path C:\Temp\file.txt
Test-Path C:\Temp -PathType Container
````

## Creating & Removing

````powershell
New-Item -Path C:\Temp\newfolder -ItemType Directory
New-Item -Path C:\Temp\file.txt -ItemType File
Remove-Item C:\Temp\file.txt
Remove-Item C:\Temp\folder -Recurse -Force
Copy-Item C:\a.txt C:\b.txt
Copy-Item C:\folder C:\dest -Recurse
Move-Item C:\a.txt C:\Temp\a.txt
Rename-Item C:\a.txt newname.txt
````

## Reading & Writing Content

````powershell
Get-Content C:\file.txt
Get-Content C:\file.txt -Raw       # Whole file as single string
Get-Content C:\file.txt -Tail 10   # Last 10 lines
Get-Content C:\file.txt -Wait      # Like `tail -f`

Set-Content C:\file.txt "New content"   # Overwrite
Add-Content C:\file.txt "Extra line"    # Append

Out-File -FilePath C:\file.txt -InputObject $data
$data | Out-File C:\file.txt -Append -Encoding UTF8
````

## File Metadata

````powershell
$f = Get-Item C:\file.txt
$f.Length; $f.LastWriteTime; $f.CreationTime; $f.Extension; $f.FullName
Get-ChildItem | Where-Object { $_.LastWriteTime -gt (Get-Date).AddDays(-7) }
````

## Paths

````powershell
Join-Path C:\Temp "file.txt"       # -> C:\Temp\file.txt
Split-Path C:\Temp\file.txt        # -> C:\Temp
Split-Path C:\Temp\file.txt -Leaf  # -> file.txt
[System.IO.Path]::GetExtension("file.txt")    # -> .txt
Resolve-Path .\relative\path
````

## Hashing & Comparing Files

````powershell
Get-FileHash C:\file.txt -Algorithm SHA256
````

## Zipping

````powershell
Compress-Archive -Path C:\Temp\* -DestinationPath C:\archive.zip
Expand-Archive -Path C:\archive.zip -DestinationPath C:\Temp\extracted
````

## Registry (Windows)

````powershell
Get-ChildItem HKLM:\SOFTWARE
Get-ItemProperty -Path "HKLM:\SOFTWARE\MyApp"
Set-ItemProperty -Path "HKLM:\SOFTWARE\MyApp" -Name "Version" -Value "2.0"
New-Item -Path "HKLM:\SOFTWARE\MyApp"
Remove-Item -Path "HKLM:\SOFTWARE\MyApp" -Recurse
````

## File System Watcher (event-driven monitoring)

````powershell
$watcher = New-Object System.IO.FileSystemWatcher "C:\Temp", "*.txt"
$watcher.EnableRaisingEvents = $true
Register-ObjectEvent $watcher Changed -Action { Write-Host "$($Event.SourceEventArgs.FullPath) changed" }
````
