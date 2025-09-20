# 🧹 BinObjCleaner

✨ A tiny **.NET 6 console app** that nukes all your  
`bin`, `obj`, and `.vs` folders (recursively) — keeping your projects **fresh & tidy**.  

Great for cleaning up **Visual Studio / .NET solutions** before commits, sharing, or just reclaiming space.

---

## 🚀 Features
- 🗑️ Delete `bin`, `obj`, `.vs` — or **any custom folder names**
- ⚙️ Configurable via **appsettings.json** (no command-line required)
- 🔍 **Dry-run mode** to preview before deleting
- ⚡ Parallel deletion (speed up cleanup using multiple threads)
- 🔒 Handles **read-only files** (common on Windows)
- 🛡️ Safe: never touches anything outside your chosen **SourcePath**

---

## 🛠️ Configuration

Open **`appsettings.json`** and set your preferences:

```json
{
  "CleanerSettings": {
    "SourcePath": "D:\\Projects\\MySolution",
    "Names": [ "bin", "obj", ".vs" ],
    "DryRun": true,
    "Parallelism": 4
  }
}
```

🔑 **Keys Explained**:
- **SourcePath** → Root folder to scan  
- **Names** → List of folder names to delete (`bin`, `obj`, `.vs`, `artifacts`, etc.)  
- **DryRun** → `true` = preview only, `false` = actually delete  
- **Parallelism** → Number of parallel workers (default = CPU count)  

---

## ▶️ Usage

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build

# Run (uses appsettings.json config)
dotnet run
```

---

## ⚠️ Notes
- 🗂️ The `.vs` folder contains Visual Studio caches and user prefs.  
  It’s safe to delete (VS will recreate it), but you may lose:
  - Window layouts  
  - Breakpoints  
  - Debug settings  

- ➕ You can add extra folders like `"artifacts"`, `"TestResults"`, etc.  

- ✅ Always run with **DryRun = true** first, to see what will be deleted.

---

👉 Now your repo stays **clean, lean, and green** 🌱  
