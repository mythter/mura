# Basics & Help System

## Getting Help

````powershell
Get-Help Get-Process              # Basic help
Get-Help Get-Process -Full        # Full help with all sections
Get-Help Get-Process -Examples    # Just examples
Get-Help Get-Process -Online      # Opens web docs
Update-Help                       # Downloads latest help files (run as admin)
Get-Command *service*             # Find cmdlets by keyword
Get-Command -Verb Get             # List all "Get" cmdlets
Get-Command -Module ActiveDirectory
````

## Discovering Objects

````powershell
Get-Member                        # Show properties/methods of piped object
Get-Process | Get-Member
(Get-Process)[0] | Get-Member -MemberType Method
````

## Aliases

````powershell
Get-Alias                         # List all aliases
Get-Alias ls                      # What does 'ls' map to?
Get-Alias -Definition Get-ChildItem
New-Alias -Name gci2 -Value Get-ChildItem
````

Common built-ins:

* `ls`/`dir` → `Get-ChildItem`
* `cat` → `Get-Content`
* `cp` → `Copy-Item`
* `rm`/`del` → `Remove-Item`
* `pwd` → `Get-Location`
* `cd` → `Set-Location`
* `echo` → `Write-Output`
* `ps` → `Get-Process`
* `kill` → `Stop-Process`
* `%` → `ForEach-Object`
* `?` → `Where-Object`

## Comments

````powershell
# Single line comment

<#
  Multi-line
  comment block
#>

<#
.SYNOPSIS
  Short description (used in comment-based help for functions)
.DESCRIPTION
  Longer description
.EXAMPLE
  My-Function -Name "Test"
#>
````

## Tab Completion & Console Tips

* `Tab` cycles through matching cmdlet/param/file names.
* `Ctrl+Space` shows a menu of completions (PSReadLine).
* `F7` shows command history as a popup (Windows console host).
* `$PSVersionTable` shows PowerShell/OS version info.
* `Clear-Host` or `cls` clears the screen.
* `exit` closes the session.

## Profiles

````powershell
$PROFILE                          # Path to current user's profile script
Test-Path $PROFILE
New-Item -Path $PROFILE -ItemType File -Force
notepad $PROFILE                  # Edit startup script (aliases, functions, etc.)
````

## Running Scripts

````powershell
.\script.ps1                      # Run a script (needs ./ prefix on the path)
powershell -File script.ps1       # Run from cmd/another shell
powershell -Command "Get-Process" # Run inline command
pwsh -NoProfile -NonInteractive -File script.ps1
````
