$mdbookSetupFolder = ".mdbook"
$mdbookFolder = "mdbook_gen"
$exportFolder = ".exported"

# removing obsidian export if it already exists
if (Test-Path $exportFolder) {
    Remove-Item $exportFolder -Recurse -Force
}
# removing mdbook folder if it already exists
if (Test-Path $mdbookFolder) {
    Remove-Item $mdbookFolder -Recurse -Force
}

# creating folder for obsidian export, removing it if it already exists
New-Item -ItemType Directory -Path $exportFolder | Out-Null

# exporting obsidian vault to a temporary folder
obsidian-export ./ $exportFolder

Copy-Item $mdbookSetupFolder $exportFolder -Recurse

# creating folder for mdbook, removing it if it already exists
New-Item -ItemType Directory -Path $mdbookFolder | Out-Null

# converting exported obsidian vault to mdbook format
dotnet run .scripts/obsidian_to_mdbook.cs -- (Resolve-Path $exportFolder) (Resolve-Path $mdbookFolder)

# removing temporary exported obsidian vault folder
Remove-Item $exportFolder -Recurse -Force


# Remove-Item "mdbook_gen" -Recurse -Force


