#!/usr/bin/env dotnet-script

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.IO.Enumeration;

// ─── Constants ───────────────────────────────────────────────────────────────────

const string MDBOOK_SETUP_DIRECTORY = ".mdbook";
const string MDBOOK_SRC_DIRECTORY = "src";
const int INDENT_SIZE = 4;
HashSet<string> EXCLUDE_FILES = new(StringComparer.OrdinalIgnoreCase) { "*.cs" };
HashSet<string> RESOURCES_DIRECTORIES = new(StringComparer.OrdinalIgnoreCase) { "_assets", "_resources" };
HashSet<string> CHAPTER_FILE_NAMES = new(StringComparer.OrdinalIgnoreCase) { "index.md", "README.md" };


// ─── Paths ───────────────────────────────────────────────────────────────────

// obsidian-export has already converted [[WikiLinks]] → regular links.
// This script takes the exported/ folder as input and writes to book_src/.
var exportedDir = Path.GetFullPath(args.Length > 0 ? args[0] : "exported");
var outputDir   = Path.GetFullPath(args.Length > 1 ? args[1] : "book_src");

var mdBookSrcPath = Path.Combine(outputDir, MDBOOK_SRC_DIRECTORY);

Console.WriteLine($"📂 Source : {exportedDir}");
Console.WriteLine($"📁 Output : {outputDir}");

if (!Directory.Exists(exportedDir))
{
    Console.Error.WriteLine($"❌ Directory not found: {exportedDir}");
    Environment.Exit(1);
}

if (Directory.Exists(outputDir))
    Directory.Delete(outputDir, recursive: true);

Directory.CreateDirectory(outputDir);

// ─── Copy mdbook initial files ──────────────────────────────────────────────────────

var mdBookInitialPath = Path.Combine(exportedDir, MDBOOK_SETUP_DIRECTORY);

foreach (var srcFile in Directory.EnumerateFiles(mdBookInitialPath, "*", SearchOption.AllDirectories))
{
    if (IsExcluded(srcFile, EXCLUDE_FILES))
    {
        continue;
    }

    var relative = Path.GetRelativePath(mdBookInitialPath, srcFile);
    var dstFile  = Path.Combine(outputDir, relative);

    Directory.CreateDirectory(Path.GetDirectoryName(dstFile)!);
    File.Copy(srcFile, dstFile, overwrite: true);
}

// ─── File Conversion ──────────────────────────────────────────────────────

int converted = 0;
int skipped   = 0;

foreach (var srcFile in Directory.EnumerateFiles(exportedDir, "*", SearchOption.AllDirectories))
{
    // skip if excluded
    if (IsExcluded(srcFile, EXCLUDE_FILES))
    {
        skipped++;
        continue;
    }

    if (HasFolderInPath(srcFile, MDBOOK_SETUP_DIRECTORY))
    {
        continue;
    }

    var relative = Path.GetRelativePath(exportedDir, srcFile);
    var dstFile  = Path.Combine(outputDir, relative);

    // Non-markdown files are copied as-is
    if (!Path.GetExtension(srcFile).Equals(".md", StringComparison.OrdinalIgnoreCase) ||
        Path.GetFileNameWithoutExtension(srcFile).Equals("README", StringComparison.OrdinalIgnoreCase)) // also copy README
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dstFile)!);
        File.Copy(srcFile, dstFile, overwrite: true);
        continue;
    }

    dstFile = Path.Combine(mdBookSrcPath, relative);

    Directory.CreateDirectory(Path.GetDirectoryName(dstFile)!);

    try
    {
        var text = File.ReadAllText(srcFile, Encoding.UTF8);
        text = StripFrontmatter(text);
        text = ConvertCallouts(text);
        text = FixImagePaths(text, relative);
        text = RemoveObsidianTags(text);
        text = NormalizeHeadings(text);

        File.WriteAllText(dstFile, text, Encoding.UTF8);
        converted++;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"⚠️  Error processing {relative}: {ex.Message}");
        skipped++;
    }
}


// ─── Asset Copying ─────────────────────────────────────────────────────

foreach (var dir in RESOURCES_DIRECTORIES)
{
    var source = Path.Combine(exportedDir, dir);

    if (!Directory.Exists(source))
        continue;

    var dest = Path.Combine(outputDir, dir);

    CopyDirectory(source, dest);
}


// ─── SUMMARY.md Generation ────────────────────────────────────────────────────

var summaryPath = Path.Combine(mdBookSrcPath, "SUMMARY.md");
var summary     = BuildSummary(mdBookSrcPath);
File.WriteAllText(summaryPath, summary, Encoding.UTF8);

Console.WriteLine($"\n✅ Done: {converted} files converted, {skipped} skipped");
Console.WriteLine($"📋 SUMMARY.md written: {summaryPath}");


// ────────────────────────────────────────────────────────────────────────────
// FUNCTIONS
// ────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Removes YAML frontmatter at the beginning of the file (--- ... ---).
/// mdBook doesn't render it nicely, so we remove it completely.
/// If you need to preserve the title — it should already be in H1.
/// </summary>
static string StripFrontmatter(string text)
{
    return Regex.Replace(
        text,
        @"^---\s*\n.*?\n---\s*\n?",
        "",
        RegexOptions.Singleline
    ).TrimStart();
}

/// <summary>
/// Converts Obsidian callouts to HTML blocks with CSS classes.
///
/// Input:
///   > [!NOTE] Title
///   > Note text
///
/// Output:
///   <div class="callout callout-note">
///   <p class="callout-title">📝 Title</p>
///   Note text
///   </div>
/// </summary>
static string ConvertCallouts(string text)
{
    // Icons for callout types
    var icons = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["note"]      = "📝",
        ["info"]      = "ℹ️",
        ["tip"]       = "💡",
        ["important"] = "❗",
        ["warning"]   = "⚠️",
        ["caution"]   = "🔥",
        ["danger"]    = "🚨",
        ["success"]   = "✅",
        ["question"]  = "❓",
        ["failure"]   = "❌",
        ["bug"]       = "🐛",
        ["example"]   = "📌",
        ["quote"]     = "💬",
        ["abstract"]  = "📄",
    };

    return Regex.Replace(
        text,
        // Capture: type, optional title, body (lines starting with ">")
        @"^> \[!(\w+)\][+-]?[ \t]*(.*?)\n((?:>[ \t]?.*\n?)*)",
        m =>
        {
            var kind   = m.Groups[1].Value.ToLower();
            var title  = m.Groups[2].Value.Trim();
            var body   = m.Groups[3].Value;
            var icon   = icons.GetValueOrDefault(kind, "📌");

            // Remove leading "> " from each line in the body
            body = Regex.Replace(body, @"^>[ \t]?", "", RegexOptions.Multiline).TrimEnd();

            if (title.Length == 0)
                title = System.Globalization.CultureInfo.InvariantCulture
                    .TextInfo.ToTitleCase(kind);

            return $"""
                <div class="callout callout-{kind}">
                <p class="callout-title">{icon} {title}</p>

                {body}
                </div>

                """;
        },
        RegexOptions.Multiline
    );
}

/// <summary>
/// Fixes image paths.
/// obsidian-export places them next to the file or in a subfolder,
/// but mdBook expects paths relative to the book_src/ root.
/// </summary>
static string FixImagePaths(string text, string relativeFilePath)
{
    // Normalize: all backslashes → forward slashes
    return Regex.Replace(
        text,
        @"!\[([^\]]*)\]\(([^)]+)\)",
        m =>
        {
            var alt  = m.Groups[1].Value;
            var path = m.Groups[2].Value.Replace('\\', '/');

            // Already absolute or external link — leave unchanged
            if (path.StartsWith("http") || path.StartsWith("/"))
                return m.Value;

            return $"![{alt}]({path})";
        }
    );
}

/// <summary>
/// Removes inline Obsidian tags (#tag in the middle of text).
/// Tags at the beginning of lines (like headings) are left untouched.
/// </summary>
static string RemoveObsidianTags(string text)
{
    // Remove #tag only if it's not at the beginning of the line (i.e., not H1-H6)
    return Regex.Replace(text, @"(?<!\n)(?<!\A)\s#[a-zA-Zа-яА-Я0-9_/-]+", "");
}

/// <summary>
/// If the file doesn't start with H1, add it from the filename.
/// mdBook uses the first H1 as the page title.
/// </summary>
static string NormalizeHeadings(string text)
{
    // If H1 already exists — do nothing
    if (Regex.IsMatch(text, @"^#\s+", RegexOptions.Multiline))
        return text;

    return text; // title will be added from SUMMARY.md
}

static void CopyDirectory(string sourceDir, string destinationDir, bool overwrite = true)
{
    if (!Directory.Exists(sourceDir))
        throw new DirectoryNotFoundException($"Source directory not found: {sourceDir}");

    // create destination directory if it doesn't exist
    Directory.CreateDirectory(destinationDir);

    // copy files
    foreach (var file in Directory.GetFiles(sourceDir))
    {
        var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
        File.Copy(file, destFile, overwrite);
    }

    // copy subfolders
    foreach (var directory in Directory.GetDirectories(sourceDir))
    {
        var destDir = Path.Combine(destinationDir, Path.GetFileName(directory));
        CopyDirectory(directory, destDir, overwrite);
    }
}

static string? NormalizeFilePath(string? path)
{
    if (string.IsNullOrWhiteSpace(path))
        return null;

    return string.Join("/", path
        .Replace('\\', '/')
        .Split('/')
        .Select(Uri.EscapeDataString));
}

static bool IsExcluded(string path, IEnumerable<string> patterns)
{
    var fileName = Path.GetFileName(path);

    return patterns.Any(p =>
        FileSystemName.MatchesSimpleExpression(p, fileName, ignoreCase: true));
}

static bool HasFolderInPath(string filePath, string folderName)
{
    var dir = new DirectoryInfo(Path.GetDirectoryName(filePath)!);

    while (dir != null)
    {
        if (string.Equals(dir.Name, folderName, StringComparison.OrdinalIgnoreCase))
            return true;

        dir = dir.Parent;
    }

    return false;
}

/// <summary>
/// Builds SUMMARY.md — a required file for mdBook.
/// Structure: first index.md (if exists), then recursively folders and files.
/// </summary>
string BuildSummary(string rootDir)
{
    var sb = new StringBuilder();
    sb.AppendLine("# Summary");
    sb.AppendLine();

    WalkDirectory(new DirectoryInfo(rootDir), sb, 0, rootDir);

    return sb.ToString();
}

void WalkDirectory(DirectoryInfo dir, StringBuilder sb, int depth, string root)
{
    var pad = new string(' ', depth * INDENT_SIZE);

    // Folders — recursively
    foreach (var subDir in dir.EnumerateDirectories()
                               .Where(d => !d.Name.StartsWith('.') && !RESOURCES_DIRECTORIES.Contains(d.Name))
                               .OrderBy(d => d.Name))
    {
        var chapterFile = GetChapterFile(subDir);
        var rel = chapterFile is null
            ? null
            : Path.GetRelativePath(root, chapterFile.FullName).Replace('\\', '/');

        sb.AppendLine($"{pad}- [{subDir.Name}]({NormalizeFilePath(rel)})");

        WalkDirectory(subDir, sb, depth + 1, root);
    }

    // Files
    foreach (var file in GetChapterFiles(dir).OrderBy(f => f.Name))
    {
        var rel = Path.GetRelativePath(root, file.FullName).Replace('\\', '/');
        sb.AppendLine($"{pad}- [{Path.GetFileNameWithoutExtension(file.Name)}]({NormalizeFilePath(rel)})");
    }
}

FileInfo? GetChapterFile(DirectoryInfo dir)
{
    return dir.EnumerateFiles("*.md")
              .FirstOrDefault(f => f.Name.Equals(dir.Name, StringComparison.OrdinalIgnoreCase) || CHAPTER_FILE_NAMES.Contains(f.Name));
}

IEnumerable<FileInfo> GetChapterFiles(DirectoryInfo dir)
{
    return dir.EnumerateFiles("*.md")
              .Where(f => !f.Name.Equals(dir.Name, StringComparison.OrdinalIgnoreCase) && !CHAPTER_FILE_NAMES.Contains(f.Name));
}
