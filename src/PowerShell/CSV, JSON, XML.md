# CSV, JSON, XML

## CSV

````powershell
# Import
$data = Import-Csv -Path .\data.csv
$data = Import-Csv -Path .\data.csv -Delimiter ";"
$data[0].ColumnName                      # Access by header name

# Export
$data | Export-Csv -Path .\out.csv -NoTypeInformation
$data | Export-Csv -Path .\out.csv -NoTypeInformation -Encoding UTF8
$data | Export-Csv -Path .\out.csv -Append   # Add rows to existing file

# Convert (string <-> object, no file involved)
$csvText = $data | ConvertTo-Csv -NoTypeInformation
$objects = $csvText | ConvertFrom-Csv
````

## JSON

````powershell
# Import (read file, then parse)
$json = Get-Content .\data.json -Raw | ConvertFrom-Json
$json.propertyName
$json | ConvertFrom-Json -AsHashtable    # PS 6+, returns hashtables instead of PSCustomObject

# Export
$obj | ConvertTo-Json | Out-File .\out.json
$obj | ConvertTo-Json -Depth 5           # Default depth is only 2; increase for nested objects
$obj | ConvertTo-Json -Compress          # Single line, no whitespace

# Calling a REST API that returns JSON
$response = Invoke-RestMethod -Uri "https://api.example.com/data"  # Auto-parses JSON
````

## XML

````powershell
# Import
[xml]$xml = Get-Content .\data.xml
$xml.root.child                          # Navigate like a native object
$xml.SelectNodes("//item")               # XPath query
$xml.SelectSingleNode("//item[@id='5']")

# Export
$obj | Export-Clixml -Path .\out.xml     # PowerShell-specific serialization (preserves types)
$restored = Import-Clixml -Path .\out.xml

# Build XML manually
$xmlDoc = New-Object System.Xml.XmlDocument
$root = $xmlDoc.CreateElement("root")
$xmlDoc.AppendChild($root)
$xmlDoc.Save("out.xml")

# ConvertTo-Xml (turns objects into a System.Xml.XmlDocument)
$xmlObj = Get-Process | Select -First 3 | ConvertTo-Xml
$xmlObj.Save("procs.xml")
````

## Clixml (best for preserving PowerShell objects, incl. hashtables/credentials)

````powershell
$data | Export-Clixml -Path state.xml
$restored = Import-Clixml -Path state.xml
Get-Credential | Export-Clixml -Path cred.xml   # Encrypted for current user/machine
$cred = Import-Clixml -Path cred.xml
````

## Quick Format Conversion Table

````powershell
Import-Csv data.csv | ConvertTo-Json | Out-File data.json
Get-Content data.json -Raw | ConvertFrom-Json | Export-Csv data.csv -NoTypeInformation
````

## Working with Nested JSON

````powershell
$json = '{"user":{"name":"Bob","roles":["admin","user"]}}' | ConvertFrom-Json
$json.user.name          # "Bob"
$json.user.roles[0]      # "admin"
````
