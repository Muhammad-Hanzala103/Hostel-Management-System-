# 📁 Portfolio — Project Evaluation Report

---

## 👤 Developer Profile

| Field | Details |
|---|---|
| **Name** | Muhammad Hanzala |
| **GitHub** | [@Muhammad-Hanzala103](https://github.com/Muhammad-Hanzala103) |
| **Repository** | [C-Sharp-Project](https://github.com/Muhammad-Hanzala103/C-Sharp-Project) |
| **Project** | Hostel Management System v2.1 |
| **Language** | C# 13 / .NET 10 |
| **Type** | Console-Based Application |

---

## 🏨 Project Summary

The **Hostel Management System** is a fully-featured, production-quality console application that manages all operational aspects of a student hostel. The system was developed in three phases:

- **v1.0** — Basic CRUD for students, rooms, payments, and complaints
- **v2.0** — Major overhaul with 50 professional-grade improvements including security hardening, analytics, scheduling, inventory, Docker containerization, and CI/CD
- **v2.1** — Architecture modernization: Upgraded to **.NET 10**, implemented **Dependency Injection (DI)**, and hardened security with modern **PBKDF2 static methods** and **FixedTimeEquals** comparison.

---

## ✅ Evaluation Checklist Coverage

| # | Requirement | Status | Details |
|---|---|---|---|
| 1 | Portfolio | ✅ | This document |
| 2 | Well-structured console app | ✅ | Multi-project, layered architecture, 16 menus |
| 3 | Dockerfile | ✅ | Multi-stage build, SDK + runtime images |
| 4 | Detailed README report | ✅ | Full README.md with all sections |
| 5 | Practical Assignment | ✅ | See section below |

---

## 🏗️ System Architecture

### Solution Structure

```
HostelManagement.sln
├── Hostel.Core/          → Business Logic Layer
├── Hostel.ConsoleApp/    → Presentation Layer  
└── Hostel.Tests/         → Unit Test Layer
```

### Layered Design

```
┌─────────────────────────────────┐
│      Hostel.ConsoleApp          │   ← User Interface
│  (Menus, Input, Display)        │
└────────────────┬────────────────┘
                 │ uses
┌────────────────▼────────────────┐
│        Hostel.Core              │   ← Business Logic
│  (Entities, Services, Repos)    │
└────────────────┬────────────────┘
                 │ reads/writes
┌────────────────▼────────────────┐
│       JSON File System          │   ← Data Layer
│  (data/*.json  backups/)        │
└─────────────────────────────────┘
```

---

## 💡 Key Technical Decisions

### 1. JSON File Storage (No Database Server)
**Why**: Zero external dependencies. The application runs anywhere .NET 10 runs — no SQL Server, no PostgreSQL, no connection strings. Perfect for evaluation and demonstration.

**How**: `JsonFileRepository<T>` — a generic repository that reads/writes any entity to its own JSON file. Uses reflection to handle the `Id` property generically.

```csharp
public class JsonFileRepository<T> : IGenericRepository<T> where T : class
{
    // Automatically assigns IDs, persists to JSON on every save
    public Task AddAsync(T entity) { ... }
    public Task SaveChangesAsync() { SaveToFile(); ... }
}
```

### 2. Microsoft Dependency Injection (DI)
The application uses the enterprise-standard `Microsoft.Extensions.DependencyInjection` container. This allows for loose coupling between services and repositories, making the code more testable and maintainable.

### 3. Modern PBKDF2 Password Security
Passwords are **never stored in plain text**. v2.1 uses the modern `Rfc2898DeriveBytes.Pbkdf2` static method for performance and security, along with `CryptographicOperations.FixedTimeEquals` to prevent timing attacks.
- 100,000 iterations (NIST recommendation)
- 16-byte cryptographically random salt
- 32-byte derived key (SHA256)

### 4. Partial Class Pattern
The `HostelApp` class spans 7 files (HostelApp.cs, HostelApp.Students.cs, etc.) using C# partial classes. This keeps each file focused (~500 lines each) while the compiler treats them as one class.

### 5. Full Async/Await
All service calls and I/O are `async/await`. This makes the architecture ready for real database or network I/O with no refactoring needed.

---

## 📊 Project Statistics

| Metric | Value |
|---|---|
| **Total Lines of Code** | ~7,000+ |
| **Entity Classes** | 17 |
| **Enums** | 14 |
| **Service Classes** | 20+ |
| **Menu Screens** | 16 main + 60+ sub-screens |
| **Unit Tests** | 16 (100% passing) |
| **Security Features** | 6 |
| **Features Total** | 50+ |
| **Data Files** | 17 JSON files |
| **Design Patterns Used** | 10 |

---

## 🔑 Feature Highlights (Top 10)

### 1. 📊 Real-Time Analytics Dashboard
The dashboard aggregates data across all modules in real time:
- Active students, rooms, occupancy rate, staff, complaints
- Total and monthly revenue with month-over-month trend (▲/▼ %)
- ASCII bar chart of 6-month revenue history
- Occupancy forecasting using trend analysis

### 2. 🔒 PBKDF2 Security
Industry-standard password hashing with 100K iterations, random salt, and lockout protection. Passwords are protected even if the JSON files are accessed directly.

### 3. 💾 Backup & Restore System
Creates timestamped backup folders of all data files. Admins can list and restore any previous backup from within the app.

### 4. 📁 Formatted Report Export
Generates neatly formatted plain-text reports (PDF-style) exported to the `exports/` folder. Reports include student lists, financial summaries, complaint status, and staff rosters.

### 5. 🎨 Theme Switching
Three built-in color themes: **Dark** (default), **Light**, and **Blue**. Theme is persisted in `appsettings.json` and applied on startup.

### 6. 📅 Attendance with Statistics
Marks daily attendance (Present/Absent/Leave/Late) and calculates attendance percentage per student. Useful for identifying students with poor attendance.

### 7. 🏖️ Leave Management Workflow
Students submit leave requests; admins approve or reject. Full approval workflow with status tracking and approver name logged in audit trail.

### 8. 📦 Asset Inventory
Tracks hostel physical assets (furniture, electronics, etc.) by room with condition rating and purchase cost. Calculates total asset value automatically.

### 9. 📝 Complete Audit Trail
Every meaningful action (login, logout, data change, backup) is automatically logged with username, module, timestamp, and details. This creates a tamper-evident audit record.

### 10. 🔄 Student Check-In/Out
Records daily entry/exit of students with timestamps and remarks. Useful for hostel curfew management and security.

---

## 🐳 Docker Support

### Dockerfile (Multi-Stage Build)

```dockerfile
# Stage 1 — Build (uses SDK image)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for better layer caching
COPY HostelManagement.sln ./
COPY Hostel.Core/Hostel.Core.csproj Hostel.Core/
COPY Hostel.ConsoleApp/Hostel.ConsoleApp.csproj Hostel.ConsoleApp/
COPY Hostel.Tests/Hostel.Tests.csproj Hostel.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Build and publish the application
RUN dotnet publish Hostel.ConsoleApp/Hostel.ConsoleApp.csproj -c Release -o /app --no-restore

# Stage 2 — Runtime (uses much smaller runtime image)
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .

# Create data directory and set permissions (if needed)
RUN mkdir -p data backups exports logs

ENTRYPOINT ["dotnet", "Hostel.ConsoleApp.dll"]
```

### Why Multi-Stage?
| | Build Image | Runtime Image |
|---|---|---|
| **Base** | `dotnet/sdk:10.0` | `dotnet/runtime:10.0` |
| **Contains** | Full SDK, compiler, tools | Runtime only |
| **Size** | ~800 MB | ~220 MB |
| **Shipped** | ❌ No | ✅ Yes |

The multi-stage approach ensures the final Docker image is small and secure — no SDK or build tools are exposed in production.

### Run Commands

```bash
# Build image
docker build -t hostel-management .

# Run interactively
docker run -it hostel-management

# With persistent data
docker run -it -v hostel_data:/app/data hostel-management
```

---

## 🧪 Unit Tests

The `Hostel.Tests` project covers core business rules:

| Test | Purpose |
|---|---|
| `RegisterStudent_ShouldSetJoinDate` | Verifies student registration sets join date |
| `AssignRoom_ShouldIncrementOccupancy` | Verifies room occupancy updates on assignment |
| `AssignRoom_ToFullRoom_ShouldThrow` | Verifies capacity enforcement |
| `RecordPayment_ShouldGenerateReceipt` | Verifies receipt number generation |
| `GetTotalRevenue_ShouldSumPaidOnly` | Verifies revenue calculation logic |
| `CreateComplaint_ShouldSetOpenStatus` | Verifies complaint initial status |
| `UpdateComplaint_ToResolved_ShouldSetResolvedAt` | Verifies resolution timestamp |
| `MarkAttendance_ShouldPersist` | Verifies attendance records |
| `GetAttendancePercentage_ShouldCalculate` | Verifies percentage calculation |
| ... + 7 more | Covering rooms, staff, fees, mess menu |

**Run with:**
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## 🎓 What I Learned

### C# Concepts Applied
- **Dependency Injection (DI)** — Using `ServiceCollection` and `ServiceProvider` for enterprise-grade architecture.
- **Generics** — `IGenericRepository<T>`, `JsonFileRepository<T>`
- **Interfaces** — 14 distinct service interfaces for loose coupling
- **Async/Await** — All I/O operations are non-blocking
- **LINQ** — Extensive use for filtering, sorting, grouping, and aggregation
- **Partial Classes** — Splitting large class across multiple files
- **Pattern Matching** — C# switch expressions and `is not null` patterns
- **Records/Tuples** — Return multiple values from methods
- **Exception Handling** — `try/catch` with meaningful error messages
- **Reflection** — Used in `JsonFileRepository<T>` for generic ID handling

### Software Engineering Principles
- **SOLID Principles** — Especially **Dependency Inversion** via DI container.
- **DRY** — One generic repository replaces 17 separate ones
- **Separation of Concerns** — Core logic (Core) completely separate from UI (ConsoleApp)
- **Layered Architecture** — Presentation (Console) → Business Logic (Core) → Data (JSON)
- **Security by Design** — PBKDF2, lockout, timeout, and fixed-time comparison built in.

---

## 🤖 Practical Assignment #1: AI-Based Application in .NET

In addition to the main Hostel Management System, I have developed a separate AI-powered chatbot application that fulfills the Practical Assignment #1 requirements for AI-Based Application Development in .NET.

### AI Chatbot Project Overview

**Location**: Separate repository/directory `AI-Chatbot-Assignment/`  
**Technology**: .NET 10 Console Application with OpenAI API integration  
**Features**:
- Interactive conversational AI using GPT-3.5-turbo
- Context-aware responses focused on hostel management
- REST API integration with proper authentication
- Asynchronous programming with HttpClient
- Error handling and user-friendly interface

### Key Implementation Details

#### API Integration
```csharp
private static async Task<string> GetChatResponse(List<object> messages)
{
    var requestBody = new
    {
        model = "gpt-3.5-turbo",
        messages = messages,
        max_tokens = 150,
        temperature = 0.7
    };

    var json = JsonSerializer.Serialize(requestBody);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync(OpenAiUrl, content);
    response.EnsureSuccessStatusCode();

    // Parse JSON response and extract AI message
    var responseString = await response.Content.ReadAsStringAsync();
    var responseJson = JsonDocument.Parse(responseString);

    return responseJson.RootElement
        .GetProperty("choices")[0]
        .GetProperty("message")
        .GetProperty("content")
        .GetString() ?? "Sorry, I couldn't generate a response.";
}
```

#### Docker Support
The AI chatbot includes a multi-stage Dockerfile for containerized deployment:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY AIChatbot.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AIChatbot.dll"]
```

### Assignment Deliverables

| Deliverable | Status | Location |
|---|---|---|
| Theory Report | ✅ | `AI-Chatbot-Assignment/Theory_Report.md` |
| Practical Report | ✅ | `AI-Chatbot-Assignment/Practical_Report.md` |
| Source Code | ✅ | `AI-Chatbot-Assignment/AIChatbot/` |
| Dockerfile | ✅ | `AI-Chatbot-Assignment/Dockerfile` |
| README | ✅ | `AI-Chatbot-Assignment/README.md` |

### AI Concepts Demonstrated

| Concept | Implementation |
|---|---|
| AI API Integration | OpenAI GPT-3.5-turbo via REST API |
| Asynchronous Programming | `async/await` for non-blocking API calls |
| JSON Processing | `System.Text.Json` for request/response handling |
| Error Handling | Try/catch with user-friendly error messages |
| HTTP Client Usage | `HttpClient` with proper headers and authentication |
| Containerization | Docker multi-stage build for deployment |

---

## 📋 Practical Assignment #1 Reference

This project directly demonstrates the following practical concepts:

| Assignment Topic | Implementation |
|---|---|
| OOP (Classes, Inheritance, Encapsulation) | 17 entity classes, service hierarchy, `IsFull` computed property |
| Collections & LINQ | Extensive use of `List<T>`, `.Where()`, `.Sum()`, `.OrderBy()`, etc. |
| File I/O | `System.IO` for JSON persistence, log files, backup/restore |
| Exception Handling | All services use `try/catch` with `InvalidOperationException` |
| Interfaces | 14 service interfaces + `IGenericRepository<T>` |
| Generics | `JsonFileRepository<T>`, `IGenericRepository<T>` |
| Async Programming | `async Task` throughout all services |
| Console I/O | Color-coded menus, tables, progress bars, masked password input |
| Design Patterns | Repository, Service Layer, Dependency Injection, Factory, Strategy |
| Unit Testing | xUnit tests for all core service behavior |

---

## 🚀 How to Run (Quick Reference)

```bash
# Option 1: Direct .NET
dotnet run --project Hostel.ConsoleApp/Hostel.ConsoleApp.csproj

# Option 2: Docker
docker build -t hostel-management .
docker run -it hostel-management

# Option 3: Run tests
dotnet test
```

**Login: `admin` / `admin123`**

---

*Portfolio prepared by Muhammad Hanzala — Project Evaluation 2026*
