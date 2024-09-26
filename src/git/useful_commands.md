# Useful Git Commands

### Show the branch on which a specific commit was originally created

{{#tabs global="example" }}
{{#tab name="Windows" }}

```shell
git reflog show --all | FINDSTR <HASH>
```

{{#endtab }}
{{#tab name="Linux" }}

```shell
git reflog show --all | grep <HASH>
```

{{#endtab }}
{{#endtabs }}
