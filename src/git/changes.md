# Changes

### DELETE changes in Workspace

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

### DELETE all changes in Workspace

new

```shell
git restore .
```

old

```shell
git checkout .
```

### DELETE all changes in Index and Workspace

```shell
git reset --hard
```

```shell
git reset --hard HEAD
```

### UNSTAGE some changes (MOVE from Index (staging area) to Workspace)

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

### UNSTAGE all changes

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
