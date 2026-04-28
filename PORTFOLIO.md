# Portfolio - Hostel Management System Evaluation Report

## Developer

| Field | Value |
|---|---|
| Name | Muhammad Hanzala |
| GitHub | `Muhammad-Hanzala103` |
| Project | Hostel Management System |
| Version | 2.1.0 |
| Platform | C# 13 / .NET 10 |
| Submission Type | Console-based application with optional MVC dashboard |

## Project Summary

This repository contains a hostel management system built around a layered architecture:

- `Hostel.Core` contains entities, repository abstractions, services, and infrastructure helpers.
- `Hostel.ConsoleApp` is the primary evaluation surface and provides the full management workflow.
- `Hostel.Web` is a lightweight dashboard over the same JSON data store.
- `Hostel.Tests` validates core business rules with xUnit.

The system is designed to run without a database server. All business data is stored in JSON files, which makes the project easy to demonstrate locally and inside Docker.

## Evaluation Coverage

| Item | Status | Evidence |
|---|---|---|
| Portfolio | Complete | This document |
| Well-structured console app | Complete | `Hostel.ConsoleApp` + `Hostel.Core` |
| Dockerfile | Complete | Root `Dockerfile` |
| Detailed README | Complete | `README.md` |
| Practical submission evidence | Complete | Build, tests, docs, terminal captures |

## Architecture

```text
Console UI / MVC Dashboard
            |
            v
Business Services + Interfaces
            |
            v
Generic JSON Repositories
            |
            v
hostel_data/*.json
```

## Technical Highlights

### Architecture

- Dependency Injection with `Microsoft.Extensions.DependencyInjection`
- Generic repository pattern with `JsonFileRepository<T>`
- Service-layer separation for students, rooms, payments, complaints, staff, attendance, notices, and audit logs
- Partial-class organization for the large console UI surface

### Security and Reliability

- PBKDF2 password hashing with SHA-256
- Failed-login lockout tracking
- Audit logging for important actions
- Backup and restore workflow
- Input sanitization for report exports

### Portability

- Docker multi-stage build for the console app
- No external database dependency
- Local `.NET 10` fallback for evaluation environments without Docker

## Validation Status

Validated locally during readiness work:

- `dotnet build HostelManagement.sln -c Release`
- `dotnet test Hostel.Tests/Hostel.Tests.csproj -c Release --no-build`
- `dotnet build Hostel.Web/Hostel.Web.csproj -c Release`

Docker CLI is installed on this machine, but Docker daemon was not running during validation, so container build and runtime still need one final check once Docker Desktop or Engine is started.

## Deliverable Boundary

The AI assignment is a separate submission repository at `C:\Projects\assignment`. This hostel portfolio no longer claims that the AI chatbot source code or AI reports are part of this repository.
