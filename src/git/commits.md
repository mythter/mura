# Commits

### Undo the last commit and move changes to Workspace (changes in staging area will also move to Workspace) (*--mixed* by default)

```shell
git reset HEAD^
```

### Undo the last commit and move changes to the Index

```shell
git reset --soft HEAD^
```

### Undo the last commit and DELETE changes (uncommitted ones too)

```shell
git reset --hard HEAD^
```