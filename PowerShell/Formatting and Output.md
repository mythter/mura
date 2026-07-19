# Formatting & Output

## Output vs Display

- `Write-Output` (or bare expressions) sends objects down the pipeline – use this for data.
- `Write-Host` writes directly to the console (bypasses the pipeline) – use only for
  user-facing messages, not data you need to reuse.

```powershell
Write-Output "This goes to the pipeline"
Write-Host "This is just console text" -ForegroundColor Green
Write-Verbose "Only shown with -Verbose"
Write-Debug "Only shown with -Debug"
Write-Warning "Yellow warning text"
Write-Error "Red error text"
Write-Progress -Activity "Copying" -Status "50%" -PercentComplete 50
```

## Format-* Cmdlets (last in a pipeline – changes display only)

```powershell
Get-Process | Format-Table Name, Id, CPU -AutoSize
Get-Process | Format-Table -Wrap
Get-Process | Format-List *
Get-Process | Format-Wide Name -Column 4
```

> Once you use a `Format-*` cmdlet, the objects can no longer be
> processed further in the pipeline – put it last.

## Out-* Cmdlets (send to a destination)

```powershell
Get-Process | Out-File -FilePath procs.txt
Get-Process | Out-GridView                # Interactive GUI table (Windows / needs module)
Get-Process | Out-Printer
Get-Process | Out-Null                    # Discard output
Get-Process | Out-String                  # Convert formatted output to a single string
```

## ConvertTo-* Cmdlets (data conversion, not just display)

```powershell
Get-Process | Select-Object Name,Id | ConvertTo-Csv -NoTypeInformation
Get-Process | Select-Object Name,Id | ConvertTo-Json
Get-Process | Select-Object Name,Id | ConvertTo-Html | Out-File report.html
Get-Process | Select-Object Name,Id | ConvertTo-Xml
```

## Sorting Column Output

```powershell
Get-Process | Sort-Object CPU -Descending | Format-Table -AutoSize
```

## Custom Table Columns

```powershell
Get-Process | Format-Table Name, @{Label="Memory(MB)"; Expression={[math]::Round($_.WS/1MB,2)}}
```

## Colored Console Output

```powershell
Write-Host "Success!" -ForegroundColor Green
Write-Host "Warning!" -ForegroundColor Yellow -BackgroundColor Black
$PSStyle.Foreground.Green + "Styled text" + $PSStyle.Reset   # PS 7.2+
```

## String Building for Output

```powershell
$report = @()
$report += "Line 1"
$report += "Line 2"
$report -join "`n" | Out-File report.txt
```

## Suppressing Output

```powershell
$null = Some-Command
Some-Command | Out-Null
[void](Some-Command)
```
