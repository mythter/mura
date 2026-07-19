# Functions & Scripts

## Basic Function

````powershell
function Get-Greeting {
    param(
        [string]$Name = "World"
    )
    "Hello, $Name!"
}

Get-Greeting -Name "Alice"
````

## Parameters

````powershell
function New-User {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Username,

        [Parameter(Mandatory=$false)]
        [int]$Age = 18,

        [ValidateSet("Admin","User","Guest")]
        [string]$Role = "User",

        [ValidateRange(1,120)]
        [int]$ValidatedAge,

        [ValidateNotNullOrEmpty()]
        [string]$Required,

        [switch]$Force               # Boolean flag, use -Force to set $true
    )
    "$Username ($Age) - $Role - Force:$Force"
}
````

## Advanced Function (CmdletBinding)

````powershell
function Get-Data {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory, ValueFromPipeline=$true)]
        [string]$Id
    )
    begin   { "Starting..." }
    process { "Processing $Id" }     # Runs once per pipeline item
    end     { "Done." }
}

1,2,3 | Get-Data
````

`[CmdletBinding()]` enables common parameters: `-Verbose`, `-Debug`,
`-ErrorAction`, `-WarningAction`, `-Confirm`, `-WhatIf` (with `SupportsShouldProcess`).

## Return Values

````powershell
function Get-Square($n) {
    return $n * $n
}

# Any unassigned/unsuppressed output in a function is also "returned"
function Get-Info {
    "line 1"     # This becomes part of the output too
    "line 2"
    return "final"
}
````

## Pipeline Input

````powershell
function Show-Item {
    param(
        [Parameter(ValueFromPipeline=$true)]
        $InputObject
    )
    process { "Got: $InputObject" }
}

1,2,3 | Show-Item
````

## Script Blocks

````powershell
$sb = { param($x) $x * 2 }
& $sb 5                  # Invoke with call operator -> 10
Invoke-Command -ScriptBlock $sb -ArgumentList 5
````

## Splatting (pass many parameters as a hashtable)

````powershell
$params = @{
    Path        = "C:\Temp"
    Recurse     = $true
    ErrorAction = "SilentlyContinue"
}

Get-ChildItem @params
````

## Comment-Based Help

````powershell
function Get-Greeting {
    <#
    .SYNOPSIS
        Returns a greeting message.
    .PARAMETER Name
        The name to greet.
    .EXAMPLE
        Get-Greeting -Name "Bob"
    #>
    param([string]$Name)
    "Hello, $Name"
}

Get-Help Get-Greeting -Full
````

## Modules Made of Functions

````powershell
# Save functions in MyModule.psm1, then:
Import-Module .\MyModule.psm1
Export-ModuleMember -Function Get-Greeting   # Inside the .psm1 to control what's public
````
