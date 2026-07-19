# Control Flow

## If / ElseIf / Else

````powershell
if ($age -lt 18) {
    "Minor"
} elseif ($age -lt 65) {
    "Adult"
} else {
    "Senior"
}
````

## Switch

````powershell
switch ($value) {
    1       { "One"; break }
    2       { "Two"; break }
    default { "Other" }
}

# Switch with wildcards / regex / conditions
switch -Wildcard ($name) {
    "A*" { "Starts with A" }
}
switch -Regex ($text) {
    '^\d+$' { "All digits" }
}
switch ($num) {
    { $_ -gt 10 } { "Greater than 10" }   # Script block condition
}

# Switch over a collection processes every matching item (no auto-break)
switch (1,2,3) { {$_ -gt 1} { "match: $_" } }
````

## For Loop

````powershell
for ($i = 0; $i -lt 5; $i++) {
    "Iteration $i"
}
````

## ForEach (statement, fast, in-memory)

````powershell
foreach ($item in 1..5) {
    "Item: $item"
}
````

## ForEach-Object (pipeline, streams objects one at a time)

````powershell
1..5 | ForEach-Object { "Item: $_" }
Get-Process | ForEach-Object { $_.Name }
# Parallel (PowerShell 7+)
1..5 | ForEach-Object -Parallel { Start-Sleep 1; $_ * 2 } -ThrottleLimit 5
````

## While

````powershell
$i = 0
while ($i -lt 5) {
    "i = $i"
    $i++
}
````

## Do-While / Do-Until

````powershell
$i = 0
do {
    "i = $i"
    $i++
} while ($i -lt 5)

do {
    "i = $i"
    $i++
} until ($i -ge 5)
````

## Break / Continue

````powershell
foreach ($i in 1..10) {
    if ($i -eq 5) { break }      # Exit the loop entirely
    if ($i % 2 -eq 0) { continue } # Skip to next iteration
    $i
}
````

## Labeled Loops (break/continue out of nested loops)

````powershell
:outer foreach ($i in 1..3) {
    foreach ($j in 1..3) {
        if ($j -eq 2) { continue outer }
        "i=$i j=$j"
    }
}
````
