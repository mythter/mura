# Modules & Packages

## Exploring Modules

````powershell
Get-Module                          # Modules loaded in the current session
Get-Module -ListAvailable           # All modules installed on the system
Get-Command -Module ActiveDirectory # Cmdlets in a specific module
````

## Importing / Removing

````powershell
Import-Module ActiveDirectory
Import-Module .\MyModule.psm1
Import-Module MyModule -Force       # Reload after changes
Remove-Module MyModule
````

## PowerShell Gallery (PSGallery)

````powershell
Find-Module -Name Az                # Search the gallery
Install-Module -Name Az -Scope CurrentUser
Install-Module -Name Az -Scope AllUsers   # Requires admin
Update-Module -Name Az
Uninstall-Module -Name Az
Get-InstalledModule                 # List modules installed via Install-Module
````

### PowerShellGet v3 / Microsoft.PowerShell.PSResourceGet (newer)

````powershell
Find-PSResource -Name Az
Install-PSResource -Name Az
Update-PSResource -Name Az
````

## Trusting the Repository

````powershell
Get-PSRepository
Set-PSRepository -Name PSGallery -InstallationPolicy Trusted
````

## Module Structure

````
MyModule/
├── MyModule.psd1     # Manifest: version, author, exported functions, dependencies
├── MyModule.psm1     # Root module: function definitions
└── Public/, Private/ # Common convention for organizing functions
````

## Creating a Module Manifest

````powershell
New-ModuleManifest -Path .\MyModule.psd1 `
    -RootModule "MyModule.psm1" `
    -Author "Me" `
    -ModuleVersion "1.0.0" `
    -FunctionsToExport @("Get-Greeting")
Test-ModuleManifest .\MyModule.psd1
````

## Controlling Exports (inside .psm1)

````powershell
function Get-Greeting { "Hello" }
function InternalHelper { "not exported" }
Export-ModuleMember -Function Get-Greeting
````

## Module Auto-Loading

Modules under paths in `$env:PSModulePath` load automatically when
one of their commands is used – explicit `Import-Module` is often unnecessary.

````powershell
$env:PSModulePath -split [IO.Path]::PathSeparator
````

## Useful Built-in / Common Modules

````
Microsoft.PowerShell.Management   # Core cmdlets (Get-Process, Get-Service, ...)
Microsoft.PowerShell.Utility      # Format-*, ConvertTo-*, Measure-Object, ...
PSReadLine                        # Command-line editing/history
ActiveDirectory                   # AD cmdlets (RSAT)
Az / AzureRM                      # Azure management
AWS.Tools.*                       # AWS management
ImportExcel                       # Read/write Excel without Excel installed
````
