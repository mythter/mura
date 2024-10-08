# Log

## Filter

### By number

```shell
git log -<number>
```

### By date

```shell
git log --since=yyyy-mm-dd
```

```shell
git log --until="3 days ago"
```

```shell
git log --after=2.weeks --before=3.days
```

### By author

```shell
git log --author="Name"
```

### By commit message pattern

```shell
git log --grep="pattern"
```

### By commits range

```shell
git log <SHA>..<SHA>
```

### By file name (only shows commits that changed that file)

```shell
git log <file_name>
```

## Decoration

### Print commit with the names of changed files

```shell
git log --name-only --oneline
```
