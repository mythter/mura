# Tags

## CREATE

### Create lightweight tag

```shell
git tag <tag_name> <SHA>
```

### Create annotated tag

```shell
git tag -am "message" <tag_name> <SHA>
```

### Create a remote tag when local exists

```shell
git push origin <tag_name>
```

### Push all local tags to remote

```shell
git push origin --tags
```

## VIEW

View tag info

```shell
git show <tag_name>
```

View tags list

```shell
git tag -l
```

View tags list by pattern

```shell
git tag -l "pattern"
```

View tags list with first line of each annotation

```shell
git tag -n
```

## DELETE

### Delete local tag

```shell
git tag -d <tag_name>
```

### Delete remote tag

```shell
git tag -d origin <tag_name>
```
