# Branches

## CREATE

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

### If there is a remote branch, but you need to create a local one and link to the remote one

```shell
git branch --track <local-branch> origin/<remote-branch>
```

```shell
git branch <local-branch> origin/<remote-branch>
```

```shell
git checkout -b <local-branch> origin/<remote-branch>
```

If there is already a local one with the same name, then it will link to the remote one

```shell
git branch -f <local-branch> origin/<remote-branch>
```

### If there is a local branch and you need to create a remote one with tracking

```shell
git push -u origin <branch-name>
```

### If there are both local and remote branches and you need to make tracking

```shell
git branch -u origin/<remote-branch-name> <local-branch-name>
```

### If you need to remove tracking

```shell
git branch --unset-upstream <branch-name>
```

## MOVE

### Move branch pointer to a different commit (*destination-commit* can be a branch name)

```shell
git reset --hard <destination-commit>
```

```shell
git branch –f <branch-name> <destination-commit>
```

## DELETE

### DELETE remote branch (*remote-name* usually *origin*)

```shell
git push -d <remote-name> <branch-name>
```

\
Don't forget to do a `git fetch --all --prune` or `git fetch --all -p` on other machines after deleting the remote branch on the server.
After deleting the local branch with `git branch -d` and deleting the remote branch with `git push -d origin` other machines may still have "obsolete tracking branches" (to see them do `git branch -a`). To get rid of these do `git fetch --all --prune`

### DELETE local branch

```shell
git branch -d <branch-name>
```

The *-D* option is an alias for *--delete* *--force*, which deletes the branch "irrespective of its merged status"

```shell
git branch -D <branch-name>
```

### DELETE local remote-tracking branch

```shell
git branch -dr <remote>/<branch>
```

long

```shell
git branch --delete --remotes <remote>/<branch>
```

## RENAME

### Rename current branch

```shell
git branch -m <branch_name>
```

### Rename another branch

```shell
git branch -m  <oldbranch_name> <new_branch_name>
```

## VIEW

### View branches that was merged with the current branch

```shell
git branch --merged
```

### View remote branches that was merged with the current branch

```shell
git branch -r --merged
```

### View branches that was't merged with the current branch

```shell
git branch --no-merged
```

## PRUNE

```shell
git remote prune origin
```

short

```shell
git fetch -p
```

long

```shell
git fetch --prune
```
