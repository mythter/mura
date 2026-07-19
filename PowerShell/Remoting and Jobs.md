# Remoting & Background Jobs

## Enabling Remoting (run once, as admin, on target machine)

```powershell
Enable-PSRemoting -Force
Set-Item WSMan:\localhost\Client\TrustedHosts -Value "*" -Force  # For workgroup/non-domain setups
```

## Interactive Remote Session

```powershell
Enter-PSSession -ComputerName Server01
Enter-PSSession -ComputerName Server01 -Credential (Get-Credential)
Exit-PSSession                      # or type: exit
```

## Running Commands Remotely

```powershell
Invoke-Command -ComputerName Server01 -ScriptBlock { Get-Process }
Invoke-Command -ComputerName Server01,Server02 -ScriptBlock { hostname }
Invoke-Command -ComputerName Server01 -FilePath .\script.ps1
Invoke-Command -ComputerName Server01 -ScriptBlock { param($n) "Hi $n" } -ArgumentList "Bob"
```

## Persistent Sessions (reuse connection, keep state)

```powershell
$session = New-PSSession -ComputerName Server01
Invoke-Command -Session $session -ScriptBlock { $x = 5 }
Invoke-Command -Session $session -ScriptBlock { $x * 2 }   # $x persists -> 10
Remove-PSSession $session
Get-PSSession
```

## Copying Files Over a Session (PS 5+)

```powershell
Copy-Item -Path C:\local\file.txt -Destination C:\remote\ -ToSession $session
Copy-Item -Path C:\remote\file.txt -Destination C:\local\ -FromSession $session
```

## SSH-Based Remoting (cross-platform, PS 7+)

```powershell
Enter-PSSession -HostName server01 -UserName bob -SSHTransport
Invoke-Command -HostName server01 -UserName bob -SSHTransport -ScriptBlock { uptime }
```

## Background Jobs

```powershell
Start-Job -ScriptBlock { Get-Process }
Start-Job -Name "MyJob" -ScriptBlock { Start-Sleep 10; "done" }
Get-Job                             # List all jobs
Get-Job -Name MyJob | Receive-Job   # Retrieve output (once, unless -Keep)
Wait-Job -Name MyJob                # Block until finished
Stop-Job -Name MyJob
Remove-Job -Name MyJob
```

## Thread Jobs (lighter weight than Start-Job, PS 7+ / module)

```powershell
Start-ThreadJob -ScriptBlock { 1..5 | ForEach-Object { $_ * 2 } }
```

## Parallel ForEach-Object (PS 7+, no job management needed)

```powershell
$results = 1..10 | ForEach-Object -Parallel {
    Start-Sleep 1
    $_ * $_
} -ThrottleLimit 5
```

## Scheduled / Remote Execution Alternatives

```powershell
# Register a scheduled job to run on a timer
Register-ScheduledJob -Name "Nightly" -ScriptBlock { Get-Date } `
    -Trigger (New-JobTrigger -Daily -At "2:00AM")
Get-ScheduledJob
```

## Checking Connectivity Before Remoting

```powershell
Test-WSMan Server01
Test-NetConnection Server01 -Port 5985
```
