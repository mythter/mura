# Commits

### Undo the last commit and move changes to Working Directory (changes in staging area will also move to Working Directory) (*--mixed* by default)

```shell
git reset HEAD^
```

### Undo the last commit and move changes to the Index

```shell
git reset --soft HEAD^
```

### Undo the last commit and move changes to the Working Directory

```shell
git reset --mixed HEAD^
```

### Undo the last commit and DELETE changes (uncommitted ones too)

```shell
git reset --hard HEAD^
```