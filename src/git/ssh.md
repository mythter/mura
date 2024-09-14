# SSH

## Create SSH key

`-t` specifies the key type

`-b` specifies the key size (in bits)

`-C` for comment to help identify your ssh key (optional)

`-f`  for the file name where ssh key get saved

```powershell
ssh-keygen -t rsa -b 4096 -C "<your email>" -f $env:USERPROFILE/.ssh/<key_name>
```

## Add SSH keys to SSH Agent

```powershell
ssh-add $env:USERPROFILE/.ssh/<key_name>
```

### If previous command doesn't work try the following or see this [link](https://stackoverflow.com/a/53606760/21737287)

Check if SSH Agent service is running

```powershell
Get-Service ssh-agent
```

Stop SSH Agent service

```powershell
Stop-Service ssh-agent
```

Start SSH Agent service

```powershell
Start-Service ssh-agent
```

## Add SSH public key to the Github

- Copy content of `<key_name>.pub`
- Goto **Settings** > **SSH and GPG keys** > **New SSH Key**
- Paste your copied public key and give it a Title of your choice

## Create SSH Config file and add Host Entries

If config file not already exists then create one (`$env:USERPROFILE/.ssh/config`)

```shell
Host github.com-<username>
  HostName github.com
  User git
  IdentityFile C:/Users/<user name>/.ssh/<key_name>
  IdentitiesOnly yes
```

`Host github.com-<username>`

- This is the hostname for which these settings apply.
- Every time you connect to this host via SSH, the following configuration will be applied.
- You can use different settings for other hosts (for example, for other servers).

`HostName github.com`

- Specifies the actual hostname to connect to.

`User git`

- GitHub requires the user to be specified as `git` for all SSH connections.

`IdentityFile C:/Users/<user name>/.ssh/<key_name>`

- Specifies the path to your private SSH key, which will be used to authenticate to GitHub.

`IdentitiesOnly yes`

- Causes SSH to only use the keys specified in the configuration file (via IdentityFile) and ignore any other keys that may be loaded into the SSH agent.
- This helps if you have multiple keys and want to use a specific key for a specific host.

## Cloning existing repository or adding remote for the local one

Clone existing repository

```shell
git clone git@github.com-<username>:<username>/<repo_name>.git
```

Add remote to local repository

```shell
git remote add origin git@github.com-<username>:<username>/<repo_name>.git
```

## Finally

Don't forget to specify `user.name` and `user.email` in every repository freshly cloned or existing before.

```shell
git config user.name "Your name"
```

```shell
git config user.email "your@email.com"
```

## Usefule commands

### Show SSH key list

```powershell
ssh-add -l
```

### Delete SSH key from SSH Agent

```powershell
ssh-add -d $env:USERPROFILE/.ssh/<key_name>
```
