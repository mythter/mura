$mdbookSetupFolder = ".mdbook"
$mdbookFolder = "mdbook_gen"
$exportFolder = ".exported"

# creating folder for obsidian export, removing it if it already exists
if (Test-Path $exportFolder) {
    Remove-Item $exportFolder -Recurse -Force
}

New-Item -ItemType Directory -Path $exportFolder | Out-Null

# exporting obsidian vault to a temporary folder
obsidian-export ./ $exportFolder

Copy-Item $mdbookSetupFolder $exportFolder -Recurse

# creating folder for mdbook, removing it if it already exists
if (Test-Path $mdbookFolder) {
    Remove-Item $mdbookFolder -Recurse -Force
}

New-Item -ItemType Directory -Path $mdbookFolder | Out-Null

# converting exported obsidian vault to mdbook format
dotnet run .scripts/obsidian_to_mdbook.cs -- (Resolve-Path $exportFolder) (Resolve-Path $mdbookFolder)

mdbook test $mdbookFolder

# removing temporary exported obsidian vault folder
Remove-Item $exportFolder -Recurse -Force


# Remove-Item "mdbook_gen" -Recurse -Force


