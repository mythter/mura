# Repositories

## Mirror remote repository

1. Create a mirror clone of the remote repository
```bash
git clone --mirror git@github.com:owner/old-repo.git  
cd old-repo.git
```
2. Update the remote URL to point to the new repository
```bash
git remote set-url origin git@github.com:owner/new-repo.git
```
3. Push the mirror
```bash
git push --mirror
```

## Mirror local repository

1. Create a mirror clone of the local repository
```bash
cd old-repo
git clone --mirror . ../old-repo-mirror.git  
cd ../old-repo-mirror.git
```
2. Update the remote URL to point to the new repository
```bash
git remote set-url origin git@github.com:owner/new-repo.git
```
3. Push the mirror
```bash
git push --mirror