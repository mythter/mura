# Strings & Regex

## String Basics

```powershell
$s = "Hello, World!"
$s.Length
$s.ToUpper()
$s.ToLower()
$s.Trim()
$s.TrimStart(); $s.TrimEnd()
$s.Substring(0,5)          # "Hello"
$s.Replace("World","PS")
$s.Contains("World")
$s.StartsWith("Hello")
$s.EndsWith("!")
$s.IndexOf("World")
$s.PadLeft(20,'*')
$s.PadRight(20,'*')
$s.Split(",")
```

## Splitting & Joining

```powershell
"a,b,c" -split ","                # -> a,b,c array
"a,b;c" -split '[,;]'             # multi-char split via regex
"a b   c" -split '\s+'            # collapse whitespace
@("a","b","c") -join "-"          # -> "a-b-c"
```

## String Interpolation & Formatting

```powershell
$name = "Alice"
"Hello, $name"
"Sum is $(1+2)"                   # Expression inside subexpression
"{0} is {1}" -f $name, "here"     # -f format operator
"{0:C}" -f 19.99                  # Currency
"{0:N2}" -f 3.14159                # 2 decimal places -> 3.14
"{0:D4}" -f 7                      # Zero-padded -> 0007
$s = "score: {0:P0}" -f 0.856      # Percentage -> 86%
```

## Here-Strings

```powershell
$text = @"
Multi-line
text with $variable interpolation
"@

$literal = @'
No interpolation here: $variable
'@
```

## Regex – Match

```powershell
"abc123" -match '\d+'             # $true, sets $matches
$matches[0]                       # "123"

if ("user@example.com" -match '^[\w.]+@[\w.]+\.\w+$') { "Valid email" }

[regex]::Match("abc123", '\d+').Value
[regex]::Matches("a1b2c3", '\d') | ForEach-Object { $_.Value }
```

## Regex – Replace

```powershell
"abc123" -replace '\d+', 'X'      # -> "abcX"
[regex]::Replace("abc123", '\d+', 'X')

# Backreferences
"John Smith" -replace '(\w+) (\w+)', '$2 $1'   # -> "Smith John"
```

## Regex – Named Groups

```powershell
if ("2024-01-15" -match '(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})') {
    $matches['year']    # "2024"
    $matches['month']   # "01"
}
```

## Common Regex Patterns

```
\d       digit                \D  not digit
\w       word char             \W  not word char
\s       whitespace             \S  not whitespace
^        start of string        $   end of string
.        any char
*        0 or more     +   1 or more    ?   0 or 1
{n,m}    n to m times
[abc]    char class            [^abc]  negated class
```

## String to Number / Number to String

```powershell
[int]"42"
[double]"3.14"
(42).ToString()
(3.14159).ToString("N2")
```

## Case-Insensitive Matching

```powershell
"ABC" -match "abc"                # True by default (case-insensitive)
"ABC" -cmatch "abc"                # False (case-sensitive)
```
