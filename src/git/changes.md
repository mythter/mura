# Changes

## DELETE changes in Workspace

new

```bash
git restore <file>
```

old

```bash
git checkout -- <file>
```

```bash
git checkout <file>
```

## DELETE all changes

new

```bash
git restore .
```

old

```bash
git checkout .
```

## MOVE changes from Index (staging area) to Workspace

new

```bash
git restore --staged <file>
```

old

```bash
git reset <file>
```

```bash
git reset HEAD <file>
```

## UNSTAGE all changes

new

```bash
git restore --staged .
```

common

```bash
git reset
```

old

```bash
git reset HEAD
```

## DELETE all changes in Index and Workspace

common

```bash
git reset --hard HEAD
```
