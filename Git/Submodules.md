# Submodules

## Add submodule 

```bash
git submodule add <repo-url> <path>
```

for example

```bash
git submodule add git@github.com:company/lib.git libs/lib
```

don't forget to commit

```bash
git add .gitmodules libs/lib  
git commit -m "Add submodule"
```

## Update Submodule

```bash
git submodule update
```

### Update Submodules to the Latest Remote Commit

```bash
git submodule update --remote
```

Or a specific submodule

```bash
git submodule update --remote libs/lib
```

## Remove Submodule

```bash
git rm <path>
```

Example:

```bash
git rm libs/lib
```

Remove the remaining metadata

````tabs

tab: Windows
```powershell
Remove-Item -Recurse -Force .git\modules\libs\lib
```
tab: Linux/macOS
```bash
rm -rf .git/modules/libs/lib
```

````
Commit the changes

```
git commit -m "Remove submodule"
```

## Clone Repository with Submodules

### Clone including all submodules

```bash
git clone --recurse-submodules <repository-url>
```

### If the repository is already cloned

```bash
git submodule update --init --recursive
```