# Error Handling

## Error Types

* **Terminating errors**: stop execution of the current scope, catchable with try/catch.
* **Non-terminating errors**: reported but execution continues (most cmdlet errors by default).

## Try / Catch / Finally

````powershell
try {
    1 / 0
} catch {
    "Error: $($_.Exception.Message)"
} finally {
    "Cleanup always runs"
}
````

## Catching Specific Exception Types

````powershell
try {
    Get-Item "C:\doesnotexist.txt" -ErrorAction Stop
} catch [System.Management.Automation.ItemNotFoundException] {
    "File not found!"
} catch [System.UnauthorizedAccessException] {
    "Access denied!"
} catch {
    "Some other error: $_"
}
````

## Forcing Non-Terminating Errors to be Catchable

````powershell
# Many cmdlets emit non-terminating errors by default; force them to terminate:
Get-Item "C:\missing.txt" -ErrorAction Stop
````

## -ErrorAction Values

````
Continue          # Default; show error, continue
SilentlyContinue  # Suppress error, continue
Stop              # Turn into terminating error (catchable)
Inquire           # Prompt the user
Ignore            # Suppress and don't add to $Error
````

````powershell
Get-ChildItem C:\missing -ErrorAction SilentlyContinue
$ErrorActionPreference = "Stop"     # Change default for the whole session/script
````

## Throwing Custom Errors

````powershell
throw "Something went wrong"
throw [System.InvalidOperationException]::new("Bad state")

# Advanced: write structured, non-terminating errors
Write-Error "Custom error message" -Category InvalidArgument
````

## $Error Automatic Variable

````powershell
$Error[0]                          # Most recent error
$Error[0].Exception.Message
$Error.Clear()                     # Clear the error history
$Error.Count
````

## Inspecting the Caught Error ($\_)

````powershell
try { throw "Oops" } catch {
    $_.Exception.Message
    $_.Exception.GetType().FullName
    $_.InvocationInfo.ScriptLineNumber
    $_.ScriptStackTrace
}
````

## Trap (older style, applies to entire scope)

````powershell
trap {
    "Caught: $_"
    continue        # or 'break' to stop
}
1/0
"This still runs if 'continue' used"
````

## -WhatIf / -Confirm (safe execution pattern)

````powershell
function Remove-Stuff {
    [CmdletBinding(SupportsShouldProcess=$true)]
    param($Path)
    if ($PSCmdlet.ShouldProcess($Path, "Delete")) {
        Remove-Item $Path
    }
}
Remove-Stuff -Path "C:\Temp\file.txt" -WhatIf
Remove-Stuff -Path "C:\Temp\file.txt" -Confirm
````

## Best Practices

````powershell
# Always use -ErrorAction Stop inside try blocks when calling cmdlets
try {
    $result = Invoke-RestMethod -Uri $url -ErrorAction Stop
} catch {
    Write-Error "Request failed: $($_.Exception.Message)"
} finally {
    # Release resources, close connections, etc.
}
````
