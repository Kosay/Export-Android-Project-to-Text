# Export Project to Text

A small Windows desktop tool that dumps the source files of a project into a
single text or Markdown file — handy for pasting an entire small codebase into
an LLM prompt or sharing a snapshot for review.

Originally built for Android (Kotlin/XML) projects; now also supports
TypeScript, JavaScript, Java, JSON, Gradle, and more.

## Features

- Interactive **project tree** with checkboxes — check any folder and every
  file and subfolder inside it is checked automatically.
- Configurable **extension list** (default on: `.kt`, `.kts`, `.xml`, `.ts`,
  `.tsx`; available: `.java`, `.js`, `.jsx`, `.json`, `.gradle`, `.toml`,
  `.pro`, `.properties`, `.md`, `.txt`).
- Automatically **skips noise folders** (`build`, `.gradle`, `.idea`, `.git`,
  `node_modules`, `bin`, `obj`, `dist`, `out`, …). Toggle off if you want them.
- **Markdown output** with fenced code blocks per file (great for LLMs), or
  plain text mode.
- **Max file size** cap so a stray large asset never balloons the output.
- **Streamed output** — writes as it goes, no giant in-memory buffer.
- **Async** with progress bar, live status, and Cancel.
- **Skip-and-log** failures — one unreadable file no longer aborts the export;
  you get a summary of what was skipped and why.

## Build

Requires .NET 8 SDK on Windows.

```
dotnet build "Export Android Project.sln" -c Release
```

The built app is a WinForms `.exe` under `bin/Release/net8.0-windows/`.

## Usage

1. Click **Browse...** next to *Project folder* and pick your project root.
2. Adjust the tree — check/uncheck folders and files as you like.
3. Tick the extensions you want to include.
4. Choose an output file (default suggestion is `<project>-export.md` next to
   the project).
5. Click **Export**.

## License

See `LICENSE.txt`.
