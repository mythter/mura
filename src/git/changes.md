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

### DELETE all changes

new

```shell
git restore .
```

old

```shell
git checkout .
```

### MOVE changes from Index (staging area) to Workspace

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

### DELETE all changes in Index and Workspace

common

```shell
git reset --hard HEAD
```
