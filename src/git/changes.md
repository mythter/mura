# Changes

## STAGE

### Stage all changes

```shell
git add .
```

### Stage changes in interactive mode

```shell
git add -i
```

### Stage changes in patch mode

```shell
git add -p
```

## UNSTAGE

### Unstage some changes (MOVE from Index (staging area) to Workspace)

new

```shell
git restore --staged <file>
```

old

```shell
git reset <file>
```

```shell
git reset HEAD <file>
```

### Unstage all changes

new

```shell
git restore --staged .
```

common

```shell
git reset
```

old

```shell
git reset HEAD
```

## DELETE

### Delete changes in Workspace

new

```shell
git restore <file>
```

old

```shell
git checkout -- <file>
```

```shell
git checkout <file>
```

### Delete all changes in Workspace

new

```shell
git restore .
```

old

```shell
git checkout .
```

### Delete all changes in Index and Workspace

```shell
git reset --hard
```

```shell
git reset --hard HEAD
```

## REMOVE FROM GIT

### Remove file from Git tracking but keep it on disk

```shell
git rm --cached <file_name>
```

### Remove file from the git repository and from the local disk

```shell
git rm <file_name>
```
