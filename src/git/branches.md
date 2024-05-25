# Branches

## CREATING

### Create a branch

```shell
git branch <branch-name>
```

### Create a branch and switch to it

common
```shell
git checkout -b <branch-name> 
```

new

```shell
git switch -c <branch-name>
```

### If there is a remote branch, but you need to create a local one and link to the remote one (if there is already a local one with the same name, then it will link to the remote one)

```shell
git branch --track <local-branch> origin/<remote-branch>
```

```shell
git branch -f <local-branch> origin/<remote-branch>
```

### If there is a local branch and you need to create a remote one with tracking

```shell
git push -u origin <branch-name>
```

## MOVING

### Move branch pointer to a different commit (*destination-commit* can be a branch name)

```shell
git reset --hard <destination-commit>
```

```shell
git branch –f <branch-name> <destination-commit>
```

## DELETING

### DELETE remote branch (*remote-name* usually *origin*)

```shell
git push -d <remote-name> <branch-name>
```

### DELETE local branch

```shell
git branch -d <branch-name>
```