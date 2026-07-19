# Variables & Data Types

## Declaring Variables

```powershell
$name = "John"
$age = 30
$isAdmin = $true
$nothing = $null

[int]$count = 5                   # Explicit type
[string]$id = 42                  # Cast to string

Remove-Variable name              # Delete a variable
Clear-Variable age                # Reset to $null / default
```

## Common Types

```powershell
[int]        # 32-bit integer
[long]       # 64-bit integer
[double]     # Floating point
[decimal]    # High-precision decimal
[string]     # Text
[bool]       # $true / $false
[datetime]   # Date/time value
[array]      # Collection
[hashtable]  # Key-value pairs
[pscustomobject]  # Custom object
```

## Type Checking & Casting

```powershell
$x.GetType()                      # Get runtime type
$x -is [int]                      # Type check
[int]"42"                         # Cast string to int
[string]42                        # Cast int to string
[datetime]"2024-01-15"
```

## Strings

```powershell
$s1 = 'Single quotes: no interpolation, $name stays literal'
$s2 = "Double quotes: interpolates $name and $(1+2)"
$multi = @"
Here-string
spans multiple lines, supports "$name" interpolation
"@
$literal = @'
Literal here-string, no interpolation
'@
```

## Arrays

```powershell
$arr = 1,2,3,4
$arr = @(1,2,3,4)
$arr = @()                        # Empty array
$arr[0]                           # First element
$arr[-1]                          # Last element
$arr[1..3]                        # Slice
$arr += 5                         # Append (creates a new array under the hood)
$arr.Count / $arr.Length
[System.Collections.ArrayList]$list = @()  # Mutable, faster for many appends
$list.Add("item") | Out-Null
```

## Hashtables (Dictionaries)

```powershell
$h = @{ Name = "John"; Age = 30 }
$h["Name"]
$h.Name
$h["City"] = "NYC"                # Add/update key
$h.Remove("Age")
$h.Keys
$h.Values
$h.ContainsKey("Name")
foreach ($key in $h.Keys) { "$key = $($h[$key])" }

# Ordered hashtable (preserves insertion order)
$oh = [ordered]@{ First = 1; Second = 2 }
```

## Custom Objects

```powershell
$obj = [pscustomobject]@{
    Name = "John"
    Age  = 30
}
$obj.Name

New-Object -TypeName PSObject -Property @{ Name = "John"; Age = 30 }
```

## Variable Scope

```powershell
$global:x = 1     # Global scope
$script:y = 2     # Script scope
$local:z = 3      # Local (default) scope
function Test { $using:x }   # Access outer variable in remote/job context
```

## Environment Variables

```powershell
$env:PATH
$env:USERNAME
$env:MY_VAR = "value"             # Set for current session
[Environment]::SetEnvironmentVariable("MY_VAR","value","User")  # Persistent
```

## Automatic Variables (built-in)

```powershell
$_          # Current pipeline object
$?          # Success status of last command
$LASTEXITCODE # Exit code of the last executed external program
$Error      # Array of recent errors
$PSVersionTable # Information about the PowerShell version and runtime environment
$Args       # Arguments passed to a script/function
$Home       # User's home directory
$PWD        # Current working directory
```
