# Networking

## Connectivity Testing

```powershell
Test-Connection google.com                       # Cross-platform ping (PS 6+)
Test-Connection google.com -Count 1 -Quiet        # Returns $true/$false
Test-NetConnection google.com                     # Windows-only, richer diagnostics
Test-NetConnection google.com -Port 443
Test-NetConnection -ComputerName Server01 -Port 5985 -InformationLevel Detailed
```

## DNS

```powershell
Resolve-DnsName google.com                        # Windows
[System.Net.Dns]::GetHostAddresses("google.com")  # Cross-platform
[System.Net.Dns]::GetHostEntry("google.com")
```

## HTTP Requests

```powershell
# Invoke-RestMethod: auto-parses JSON/XML responses into objects
$data = Invoke-RestMethod -Uri "https://api.example.com/users" -Method Get
Invoke-RestMethod -Uri $url -Method Post -Body ($body | ConvertTo-Json) -ContentType "application/json"
Invoke-RestMethod -Uri $url -Headers @{ Authorization = "Bearer $token" }

# Invoke-WebRequest: raw response object (status code, headers, raw content)
$resp = Invoke-WebRequest -Uri "https://example.com"
$resp.StatusCode
$resp.Headers
$resp.Content

# Download a file
Invoke-WebRequest -Uri $url -OutFile "file.zip"
# or, faster for large files:
Start-BitsTransfer -Source $url -Destination "file.zip"   # Windows
```

## Network Adapter / IP Info (Windows)

```powershell
Get-NetIPAddress
Get-NetAdapter
Get-NetAdapter | Where-Object Status -eq "Up"
Get-DnsClientServerAddress
Get-NetRoute
ipconfig /all                                      # Still works via legacy tools
```

## Ports & Sockets

```powershell
Get-NetTCPConnection                                # Windows equivalent of netstat
Get-NetTCPConnection -State Listen
Test-NetConnection -ComputerName localhost -Port 8080
```

## Working with a TcpClient Directly

```powershell
$client = New-Object System.Net.Sockets.TcpClient
$client.Connect("example.com", 80)
$client.Connected
$client.Close()
```

## Web Sockets / SSH (PowerShell 7+)

```powershell
Enter-PSSession -HostName server01 -UserName bob -SSHTransport
```

## Sending Email (via SMTP directly – Send-MailMessage is deprecated)

```powershell
$smtp = New-Object System.Net.Mail.SmtpClient("smtp.example.com", 587)
$smtp.EnableSsl = $true
$smtp.Credentials = New-Object System.Net.NetworkCredential("user","pass")
$smtp.Send("from@example.com", "to@example.com", "Subject", "Body text")
```

## Common Status Code Handling

```powershell
try {
    $resp = Invoke-WebRequest -Uri $url -ErrorAction Stop
} catch {
    $status = $_.Exception.Response.StatusCode.value__
    "Request failed with status $status"
}
```
