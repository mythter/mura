# Areas

## **УДАЛЕНИЕ** изменений в **Workspace** (рабочая область)

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

## Удаление всех изменений

new

```bash
git restore .
```

old

```bash
git checkout .
```

## **ПЕРЕНОС** изменений из **Index** (stage область) в **Workspace** (рабочая область)

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

## Отмена всех изменений

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

## **УДАЛЕНИЕ** всех изменений в Index и Workspace

common

```bash
git reset --hard HEAD
```
