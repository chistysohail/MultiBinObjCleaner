BinObjCleaner

A .NET 6 console app that deletes all bin, obj, and .vs folders under a given path (recursively).
Useful for cleaning up Visual Studio / .NET projects.

Features
Deletes bin, obj, .vs, or any custom folder names.

Configurable via appsettings.json (no need to pass arguments).

Supports dry-run mode (preview what will be deleted).

Parallel deletion with configurable degree of concurrency.

Handles read-only files on Windows.

Safe: doesn’t touch anything outside the given SourcePath.

Configuration
Edit appsettings.json:

{
  "CleanerSettings": {
    "SourcePath": "D:\\Projects\\MySolution",
    "Names": [ "bin", "obj", ".vs" ],
    "DryRun": true,
    "Parallelism": 4
  }
}

SourcePath → Root folder to scan.

Names → Folder names to delete.

DryRun → true = only list what would be deleted, false = actually delete.

Parallelism → Number of parallel deletions (default = CPU count).

Notes
The .vs folder contains Visual Studio cache and user settings. Safe to delete, but Visual Studio will recreate it (you may lose window layout, breakpoints, etc.).

You can add more folder names to the Names list, e.g., "artifacts", "TestResults".

Run in dry-run mode first to verify before deleting.
