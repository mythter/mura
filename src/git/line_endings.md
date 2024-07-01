# Line endings

The `git config core.autocrlf` command is used to change how Git handles line endings. It takes a single argument.

```shell
git config --global core.autocrlf input|true|false
```

<br>

| autocrlf | commit, add | checkout    | When to Use                  |
|----------|-------------|-------------|------------------------------|
| input    | CRLF -> LF  | LF -> LF    | Unix, MacOS                  |
| true     | CRLF -> LF  | LF -> CRLF  | Windows                      |
| false    |      -      |      -      | Single platform              |
