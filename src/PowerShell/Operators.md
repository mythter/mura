# Operators

## Arithmetic

````powershell
5 + 3    # 8
5 - 3    # 2
5 * 3    # 15
5 / 3    # 1.666...
5 % 3    # 2 (modulo)
2 -shl 3 # bitwise left shift
16 -shr 2 # bitwise right shift
5++      # not valid; use $x++ on a variable
$x = 5; $x++   # increment
$x--           # decrement
````

## Assignment

````powershell
$x = 5
$x += 1     # 6
$x -= 1     # 5
$x *= 2     # 10
$x /= 2     # 5
$x %= 2     # 1
````

## Comparison (case-insensitive by default for strings)

````powershell
-eq   # equal
-ne   # not equal
-gt   # greater than
-ge   # greater or equal
-lt   # less than
-le   # less or equal

# Case-sensitive variants: prefix with 'c' -> -ceq, -cne, -cgt ...
# Explicit case-insensitive: prefix with 'i' -> -ieq, -ine ...

"abc" -eq "ABC"    # True (case-insensitive)
"abc" -ceq "ABC"   # False (case-sensitive)
````

## Containment & Matching

````powershell
-contains   # collection contains value:   1,2,3 -contains 2
-notcontains
-in         # value in collection:         2 -in 1,2,3
-notin
-like       # wildcard match:              "file.txt" -like "*.txt"
-notlike
-match      # regex match:                 "abc123" -match '\d+'
-notmatch
-replace    # regex replace:                "abc" -replace 'a','x'
````

## Logical

````powershell
-and
-or
-not / !
-xor

if ($a -gt 5 -and $b -lt 10) { "both true" }
````

## Type Operators

````powershell
-is         # $x -is [int]
-isnot
-as         # attempt safe cast: "42" -as [int]
````

## String / Format Operator

````powershell
"{0} is {1} years old" -f "John", 30    # -f format operator
"a" + "b"          # Concatenation -> "ab"
"ab" * 3           # Repetition   -> "ababab"
````

## Null-Coalescing (PowerShell 7+)

````powershell
$val ??= "default"        # Assign if $val is $null
$result = $val ?? "fallback"   # Use fallback if $val is $null
$x ? "yes" : "no"          # Ternary operator (PS 7+)
````

## Range & Split/Join

````powershell
1..5              # Range operator -> 1,2,3,4,5
"a,b,c" -split ","        # -> a, b, c
@("a","b","c") -join "-"  # -> "a-b-c"
````

## Redirection

````powershell
Get-Process > out.txt        # Redirect stdout, overwrite
Get-Process >> out.txt       # Append
Get-Process 2> err.txt       # Redirect errors (stream 2)
Get-Process 2>&1             # Merge error stream into stdout
Get-Process *> all.txt       # Redirect all streams
````
