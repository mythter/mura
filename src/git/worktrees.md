# Worktrees

### Show all worktrees

```shell
git worktree list
```

## CREATE

### Create a worktree for an existing branch using the same name as the working directory

```shell
git worktree add path/to/folder/<existing-branch-name>
```

### Create a worktree for an existing branch using a different name than the working directory

```shell
git worktree add path/to/folder/ <existing-branch-name>
```

### Create a worktree with a new branch using the same name as the working directory

```shell
git worktree add -b <new-branch-name> path/to/folder/<new-branch-name>
```

### Create a worktree with a new branch using a different name than the working directory

```shell
git worktree add -b <new-branch-name> path/to/folder/
```

## DELETE

## Remove a worktree

```shell
git worktree remove path/to/folder/
```
