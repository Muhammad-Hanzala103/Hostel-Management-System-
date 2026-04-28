# Hostel Management System

Console-first hostel management application built with C# and .NET 10. This repository is the hostel project submission only. The separate AI assignment lives in `C:\Projects\assignment`.

## Evaluation Checklist Mapping

| Requirement | Status | Evidence |
|---|---|---|
| Portfolio | Complete | `PORTFOLIO.md` |
| Well-structured console application | Complete | `Hostel.ConsoleApp`, `Hostel.Core`, `Hostel.Tests` |
| Dockerfile | Complete | `Dockerfile` |
| Detailed README report | Complete | This file |
| Practical assignment evidence | Complete for hostel repo | Build, tests, terminal capture, portfolio |

## Solution Structure

```text
C-Sharp-Project/
├── HostelManagement.sln
├── Hostel.Core/          # Entities, services, repositories, infrastructure
├── Hostel.ConsoleApp/    # Main console UI
├── Hostel.Web/           # Lightweight MVC dashboard over the same JSON data
├── Hostel.Tests/         # xUnit tests
├── Dockerfile
├── PORTFOLIO.md
└── CHANGELOG.md
```

## Key Features

- Student, room, payment, complaint, staff, visitor, attendance, leave, inventory, and notice management
- JSON persistence with no external database requirement
- PBKDF2 password hashing, login lockout, audit logging, and session timeout
- Dependency Injection-based architecture
- Report export, backup/restore, analytics, and seeded demo data
- Docker-ready console execution

## Runtime Requirements

### Docker-first path

Docker is the primary portability path for evaluation. A machine with Docker installed can run the app without installing the .NET SDK.

```bash
docker build -t hostel-management .
docker run -it hostel-management
```

Persistent data volume:

```bash
docker run -it -v hostel_data:/app/hostel_data -v hostel_exports:/app/hostel_exports -v hostel_backups:/app/hostel_backups hostel-management
```

### Local fallback path

Requires .NET 10 SDK.

```bash
dotnet restore HostelManagement.sln
dotnet build HostelManagement.sln -c Release
dotnet run --project Hostel.ConsoleApp/Hostel.ConsoleApp.csproj
```

### Publish fallback

Framework-dependent release output:

```bash
dotnet publish Hostel.ConsoleApp/Hostel.ConsoleApp.csproj -c Release -o publish/console
dotnet publish Hostel.Web/Hostel.Web.csproj -c Release -o publish/web
```

Run the published console app:

```bash
dotnet publish/console/Hostel.ConsoleApp.dll
```

Run the published web dashboard:

```bash
dotnet publish/web/Hostel.Web.dll
```

## First Run

The console app seeds demo data automatically on first run.

- Default username: `admin`
- Default password: `admin123`

Generated runtime folders:

- `hostel_data/`
- `hostel_exports/`
- `hostel_backups/`
- `hostel_logs/`

## Web Dashboard

`Hostel.Web` is a small MVC dashboard that reads from the same JSON data files as the console app. Use the console app for data entry, then open the web dashboard to review summary statistics.

```bash
dotnet run --project Hostel.Web/Hostel.Web.csproj
```

Demo login:

- Username: `admin`
- Password: `admin123`

## Validation Commands

```bash
dotnet build HostelManagement.sln -c Release
dotnet test Hostel.Tests/Hostel.Tests.csproj -c Release --no-build
dotnet build Hostel.Web/Hostel.Web.csproj -c Release
```

## Terminal Capture

Evaluation evidence is included as terminal captures:

- `docs/hostel-console-capture.txt`
- `docs/hostel-validation-capture.txt`

## Notes

- This repository intentionally does not contain the separate AI assignment deliverables.
- The Dockerfile publishes the console application only, because that is the primary evaluation target for this repo.
- CI is configured for .NET 10 and validates the solution, tests, and web dashboard build.
