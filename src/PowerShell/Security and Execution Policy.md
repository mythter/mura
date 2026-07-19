# Security & Execution Policy

## Execution Policy

Controls whether/how scripts run – a safety guardrail, not a full security boundary.

````powershell
Get-ExecutionPolicy
Get-ExecutionPolicy -List           # Show policy per scope
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
````

Common values:

````
Restricted     # No scripts allowed (default on client Windows)
AllSigned      # All scripts must be digitally signed
RemoteSigned   # Local scripts run freely; downloaded ones need a signature
Unrestricted   # All scripts run, warns for downloaded ones
Bypass         # Nothing blocked, no warnings
````

Scopes (checked in order): `MachinePolicy`, `UserPolicy`, `Process`, `CurrentUser`, `LocalMachine`.

````powershell
# One-off bypass without changing the persistent setting
powershell -ExecutionPolicy Bypass -File .\script.ps1
````

## Credentials

````powershell
$cred = Get-Credential                        # Prompts for username/password
$cred.UserName
$cred.GetNetworkCredential().Password          # Plaintext (use carefully)

# Build a credential without prompting (e.g., from a vault)
$securePwd = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential("user", $securePwd)
````

## SecureString

````powershell
$secure = Read-Host -AsSecureString -Prompt "Enter password"
$secure = ConvertTo-SecureString "text" -AsPlainText -Force
$plain  = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto(
    [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure))

# Persist a secure string, encrypted for the current user/machine only
$secure | ConvertFrom-SecureString | Out-File secret.txt
$loaded = Get-Content secret.txt | ConvertTo-SecureString
````

## Signing Scripts

````powershell
$cert = Get-ChildItem Cert:\CurrentUser\My -CodeSigningCert
Set-AuthenticodeSignature -FilePath .\script.ps1 -Certificate $cert[0]
Get-AuthenticodeSignature .\script.ps1
````

## Running as a Different User / Elevated

````powershell
Start-Process powershell -Verb RunAs                    # Elevate (UAC prompt)
Start-Process powershell -Credential $cred -ArgumentList "-File script.ps1"
````

## Constrained Language Mode (hardened environments)

````powershell
$ExecutionContext.SessionState.LanguageMode
# FullLanguage | ConstrainedLanguage | RestrictedLanguage | NoLanguage
````

## Secrets Management Module (recommended over plaintext files)

````powershell
Install-Module Microsoft.PowerShell.SecretManagement
Install-Module Microsoft.PowerShell.SecretStore
Register-SecretVault -Name LocalVault -ModuleName Microsoft.PowerShell.SecretStore
Set-Secret -Name "ApiKey" -Secret "abc123" -Vault LocalVault
Get-Secret -Name "ApiKey" -Vault LocalVault -AsPlainText
````

## Avoiding Common Pitfalls

* Never hardcode plaintext passwords in scripts checked into source control.
* Prefer `SecretManagement`/vaults or `Export-Clixml` (user/machine-bound encryption) over plain text.
* `ConvertTo-SecureString -AsPlainText -Force` is only as secure as the source string that fed it.
