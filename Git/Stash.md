# Stash

## SAVE

new

```shell
git stash
```

```shell
git stash push
```

```shell
git stash push -m "message"
```

old

```shell
git stash save "message"
```

## VIEW

```shell
git stash list
```

## EJECT

### Get content from stash (and delete from there)

```shell
git stash pop
```

### Get content from stash (and leave it there)

```shell
git stash apply
```

## DELETE

```shell
git stash drop
```
