# Pipeline & Object Manipulation

PowerShell's pipeline passes **objects**, not text, between cmdlets.

## Select-Object

````powershell
Get-Process | Select-Object Name, Id, CPU
Get-Process | Select-Object -First 5
Get-Process | Select-Object -Last 5
Get-Process | Select-Object -Unique
Get-Process | Select-Object -Property Name -ExpandProperty Name
Get-Process | Select-Object *                     # All properties
Get-Process | Select-Object Name, @{Name="MB";Expression={$_.WS/1MB}}  # Calculated property
````

## Where-Object (filtering)

````powershell
Get-Process | Where-Object { $_.CPU -gt 100 }
Get-Process | Where-Object CPU -gt 100             # Simplified syntax (single condition)
Get-Service | Where-Object { $_.Status -eq "Running" -and $_.Name -like "W*" }
````

## Sort-Object

````powershell
Get-Process | Sort-Object CPU
Get-Process | Sort-Object CPU -Descending
Get-Process | Sort-Object -Property Name, Id
Get-Process | Sort-Object -Unique Name
````

## Group-Object

````powershell
Get-Process | Group-Object Company
Get-ChildItem | Group-Object Extension | Sort-Object Count -Descending
````

## Measure-Object

````powershell
Get-Process | Measure-Object CPU -Sum -Average -Maximum -Minimum
Get-ChildItem | Measure-Object Length -Sum
"one two three" | Measure-Object -Word -Character -Line
````

## ForEach-Object

````powershell
Get-Process | ForEach-Object { $_.Name.ToUpper() }
1..3 | ForEach-Object -Begin { "start" } -Process { $_ } -End { "end" }
````

## Tee-Object (split pipeline output to file/variable and continue)

````powershell
Get-Process | Tee-Object -FilePath procs.txt | Where-Object CPU -gt 50
````

## Compare-Object

````powershell
Compare-Object $arr1 $arr2
Compare-Object $arr1 $arr2 -IncludeEqual -PassThru
````

## ForEach / Where as Methods (fast, PS 4+)

````powershell
(Get-Process).Where({ $_.CPU -gt 100 })
(Get-Process).ForEach({ $_.Name })
````

## Building a Custom Pipeline Chain

````powershell
Get-Process |
    Where-Object CPU -gt 50 |
    Sort-Object CPU -Descending |
    Select-Object -First 10 Name, CPU, Id |
    Format-Table -AutoSize
````

## Object Type Discovery Recap

````powershell
Get-Process | Get-Member                 # Properties & methods
(Get-Process)[0].PSObject.Properties.Name  # List property names directly
````
