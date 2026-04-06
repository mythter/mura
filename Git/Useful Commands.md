# Useful Git Commands

### Show the branch on which a specific commit was originally created


````tabs

tab: Windows
```shell
git reflog show --all | FINDSTR <HASH>
```
tab: Linux
```shell
git reflog show --all | grep <HASH>
```

````
