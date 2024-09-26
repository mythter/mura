# Useful Git Commands

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
