# 🏨 Hostel Management System
### Console-Based Application · .NET 8 · C# · Version 2.0

<div align="center">

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=for-the-badge&logo=docker)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)
![Tests](https://img.shields.io/badge/Tests-16%20Passing-brightgreen?style=for-the-badge)
![Features](https://img.shields.io/badge/Features-50+-orange?style=for-the-badge)

**A fully-featured, production-quality hostel management system with a rich console UI, JSON-based persistence, role-based security, analytics, and Docker support.**

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Architecture](#-architecture)
- [Features](#-features)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Docker Deployment](#-docker-deployment)
- [Usage Guide](#-usage-guide)
- [Security](#-security)
- [Data & Persistence](#-data--persistence)
- [Testing](#-testing)
- [Design Patterns](#-design-patterns)
- [Screenshots / Demo](#-screenshots--demo)
- [Contributing](#-contributing)
- [License](#-license)

---

## 📖 Overview

The **Hostel Management System** is a comprehensive, multi-project C# solution designed to fully manage a student hostel. It handles everything from student enrollment, room assignment, and fee collection, to staff management, visitor logging, attendance tracking, and advanced analytics — all within an elegant, color-coded console interface.

### Why This Project?

Managing a student hostel involves coordinated tracking of dozens of processes — rooms, students, fees, staff, visitors, complaints, and more. This system solves all of these with:

- **No database server needed**: Uses JSON file persistence — just run and go.
- **Secure by default**: PBKDF2 password hashing, login lockout, session timeout, and audit logging.
- **Enterprise-grade architecture**: Clean separation of concerns, interfaces, repositories, and services.
- **Data that survives restarts**: All changes persist to JSON files automatically.
- **Containerized**: Ships as a ready-to-run Docker image.

---

## 🏗️ Architecture

The solution follows a **layered architecture** with three projects:

```
HostelManagement.sln
│
├── Hostel.Core/          ← Business Logic Layer (class library)
│   ├── Entities.cs       ← Domain models + enums
│   ├── Interfaces.cs     ← Service + repository contracts
│   ├── Services.cs       ← All business service implementations
│   ├── Infrastructure.cs ← Utilities: Logger, Config, Theme, BackupService, etc.
│   └── DTOs.cs           ← Data Transfer Objects
│
├── Hostel.ConsoleApp/    ← Presentation Layer (console executable)
│   ├── Program.cs        ← App bootstrap / DI wiring
│   ├── HostelApp.cs      ← Main menu + top-level menus
│   ├── HostelApp.Students.cs  ← Student management screens
│   ├── HostelApp.Payments.cs  ← Fee & payment screens
│   ├── HostelApp.Staff.cs     ← Staff management screens
│   ├── HostelApp.Attendance.cs← Attendance tracking screens
│   ├── HostelApp.Reports.cs   ← Reports & export screens
│   └── ConsoleUI.cs           ← Reusable UI components
│
└── Hostel.Tests/         ← Unit Tests (xUnit)
    └── UnitTest1.cs      ← 16 unit tests (all passing)
```

### Dependency Flow

```
Hostel.ConsoleApp
      │
      ▼  (references)
Hostel.Core
      │
      ▼  (uses)
JSON File System  (data/)
```

`Hostel.Core` is completely independent — it can be reused by any presentation layer (console, web, API, etc.).

---

## ✨ Features

### 🎓 Student Management
| Feature | Description |
|---|---|
| Register Student | Full profile: name, CNIC, phone, email, department, guardian |
| View / Search | Search by name, registration no., phone, or department |
| Assign Room | Assign student to any available room with capacity check |
| Unassign / Swap | Unassign or swap rooms between two students |
| Deactivate | Soft-delete with automatic room occupancy adjustment |
| Booking History | Full booking history with dates per student |

### 🏠 Room Management
| Feature | Description |
|---|---|
| Create Room | Room number, floor, type, capacity, AC/bath, monthly rent |
| Room Types | Single, Double, Triple, Dormitory |
| Availability | Real-time available vs. full rooms with occupancy % |
| Capacity Guard | Prevents overbooking automatically |
| Room Profiles | View all occupants per room |

### 💰 Fee & Payment Management
| Feature | Description |
|---|---|
| Fee Structures | Per room type: rent + mess + utilities + laundry |
| Record Payment | Amount, month/year, payment method (6 options) |
| Receipt Generation | Auto-generated formatted receipt with receipt number |
| Defaulter List | List of overdue/pending payers with outstanding amounts |
| Revenue Analytics | Total revenue, monthly revenue, month-over-month change |
| Payment Methods | Cash, Bank Transfer, Online Banking, Cheque, JazzCash, EasyPaisa |

### 📋 Complaint Management
| Feature | Description |
|---|---|
| Submit Complaint | Title, description, category, priority |
| Categories | Maintenance, Electrical, Plumbing, Cleanliness, Food, Security, Room Issue |
| Priority Levels | Low / Medium / High / Critical |
| Assign to Staff | Route complaint to specific staff member |
| Status Tracking | Open → In Progress → Resolved → Closed |
| Resolution Notes | Add notes when closing a complaint |

### 👥 Staff Management
| Feature | Description |
|---|---|
| Add/Edit Staff | Full profile: CNIC, role, salary, shift |
| Staff Roles | Warden, Accountant, Maintenance, Cook, Guard, Cleaner, Manager |
| Shift Tracking | Day / Night / Rotating |
| Deactivate | Soft-delete with active count tracking |

### 🚪 Visitor Log
| Feature | Description |
|---|---|
| Visitor Check-In | Name, CNIC, relationship, purpose, student being visited |
| Visitor Pass | Auto-generated pass number (VP-YYYYMMDD-XXXX) |
| Check-Out | Records checkout time automatically |
| Daily Reports | All visitors for a specific date |
| Student History | All visitors for a specific student |

### 📅 Attendance Tracking
| Feature | Description |
|---|---|
| Mark Attendance | Present / Absent / Leave / Late per student per day |
| Daily Summary | Count of present, absent, leave for any date |
| Student Stats | Attendance percentage per student |
| History | Full attendanc history per student |

### 🍽️ Mess Menu Management
| Feature | Description |
|---|---|
| Weekly Menu | Breakfast, Lunch, Dinner per day of week |
| Add/Edit/Delete | Full CRUD on menu items |
| Today's Menu | Quick view of today's meals |
| Full Week | View entire weekly menu in one table |

### 📢 Notice Board
| Feature | Description |
|---|---|
| Post Notice | Title, content, priority, optional expiry date |
| Priority Levels | Low / Medium / High / Urgent |
| Auto-Expiry | Notices past their expiry date are hidden |
| Manage | Edit, deactivate old notices |

### 📊 Dashboard & Analytics
| Feature | Description |
|---|---|
| Real-time Stats | Active students, rooms, occupancy rate, staff, complaints |
| Revenue Cards | Total revenue, this month's revenue |
| Trend Indicator | ▲/▼ Month-over-month revenue change percentage |
| Occupancy Bar | Visual progress bar for occupancy rate |
| 6-Month Chart | ASCII bar chart of revenue trend |
| Forecasting | Occupancy prediction with trend analysis |

### 📁 Reports & Export
| Feature | Description |
|---|---|
| Student Report | Active students with room assignments |
| Room Report | All rooms with occupancy status |
| Financial Report | Revenue summary, defaulters, monthly breakdown |
| Complaint Report | Open and resolved complaints summary |
| Staff Report | Active staff by role |
| Export to File | Formatted plain-text report export to `/exports/` folder |

### 🏖️ Leave Management
| Feature | Description |
|---|---|
| Request Leave | Student leave request with start/end date and reason |
| Approve/Reject | Admin approval workflow with approver name logged |
| Status Tracking | Pending → Approved / Rejected / Cancelled |
| History | Per-student leave history with total days |

### 📦 Inventory Management
| Feature | Description |
|---|---|
| Track Assets | Furniture, Electronics, Bedding, Plumbing, Kitchen, Cleaning, Keys |
| Per Room | Assign inventory to specific rooms |
| Condition Tracking | Good / Fair / Poor |
| Total Asset Value | Calculated total value of all inventory |
| Deactivate | Soft-delete items |

### 🔄 Student Check-In/Out
| Feature | Description |
|---|---|
| Record Movement | Daily check-in and check-out per student |
| Today's Records | All movements for today |
| Student History | Complete check-in/out history per student |

### ⚙️ Admin Settings
| Feature | Description |
|---|---|
| Change Password | PBKDF2 validation + password strength check |
| System Info | App version, framework, encryption status, theme |
| Backup Data | Creates timestamped ZIP backup of all JSON data |
| Restore Backup | Restore from any previous backup |
| List Backups | View all available backups with dates |
| Change Theme | Switch between Dark / Light / Blue themes |
| About | License info, version, author |
| Check Updates | Update availability stub |

### 🔒 Security Features
| Feature | Description |
|---|---|
| PBKDF2 Hashing | 100,000 iterations, random salt per password |
| Login Lockout | Locks account after 3 failed attempts for 5 minutes |
| Session Timeout | Auto-logout after configurable idle period (default: 15 min) |
| Audit Trail | Every action logged with user, timestamp, details |
| Input Validation | All inputs validated; CSV injection prevention |
| Password Strength | Enforces minimum length, uppercase, and digit requirements |

---

## 📁 Project Structure

```
C-Sharp-Project/
├── HostelManagement.sln              # Visual Studio Solution file
├── Dockerfile                        # Docker build definition
├── README.md                         # This file
├── CHANGELOG.md                      # Version history
├── VERSION                           # Current version (2.0.0)
├── LICENSE                           # MIT License
│
├── Hostel.Core/
│   ├── Hostel.Core.csproj
│   ├── Entities.cs          # 17 entity classes, 14 enums
│   ├── Interfaces.cs        # 14 service interfaces + IGenericRepository
│   ├── Services.cs          # 20+ service class implementations (~1100 lines)
│   ├── Infrastructure.cs    # Logger, Config, Theme, Backup, Reports (~850 lines)
│   └── DTOs.cs              # Data Transfer Objects
│
├── Hostel.ConsoleApp/
│   ├── Hostel.ConsoleApp.csproj
│   ├── Program.cs           # Entry point: config, DI, seeding, app start
│   ├── ConsoleUI.cs         # Reusable console UI components
│   ├── HostelApp.cs         # Core app: login, main menu, dashboard, settings
│   ├── HostelApp.Students.cs
│   ├── HostelApp.Payments.cs
│   ├── HostelApp.Staff.cs
│   ├── HostelApp.Attendance.cs
│   └── HostelApp.Reports.cs
│
├── Hostel.Tests/
│   ├── Hostel.Tests.csproj
│   └── UnitTest1.cs         # 16 unit tests covering core services
│
└── .github/
    └── workflows/           # GitHub Actions CI/CD pipelines
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8) installed
- Windows, macOS, or Linux

### Clone & Run

```bash
# 1. Clone the repository
git clone https://github.com/Muhammad-Hanzala103/C-Sharp-Project.git
cd C-Sharp-Project

# 2. Restore dependencies
dotnet restore

# 3. Build the solution
dotnet build

# 4. Run the Console Application
dotnet run --project Hostel.ConsoleApp/Hostel.ConsoleApp.csproj
```

### First Run

On first startup, the app **auto-seeds demo data**:
- ✅ 10 sample students
- ✅ 15 rooms (Single, Double, Triple, Dormitory)
- ✅ 5 staff members
- ✅ Weekly mess menu
- ✅ Fee structures for all room types
- ✅ Sample payments and notices

**Default Login Credentials:**
```
Username: admin
Password: admin123
```

---

## 🐳 Docker Deployment

The application is fully containerized using a multi-stage build for a minimal runtime image.

### Build & Run with Docker

```bash
# Build the Docker image
docker build -t hostel-management .

# Run the container (interactive, for console input)
docker run -it hostel-management

# Run with persistent data volume (recommended)
docker run -it -v hostel_data:/app/data hostel-management
```

### Dockerfile Explained

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Hostel.Core/Hostel.Core.csproj Hostel.Core/
COPY Hostel.ConsoleApp/Hostel.ConsoleApp.csproj Hostel.ConsoleApp/
COPY Hostel.Tests/Hostel.Tests.csproj Hostel.Tests/
RUN dotnet restore Hostel.ConsoleApp/Hostel.ConsoleApp.csproj
COPY . .
RUN dotnet publish Hostel.ConsoleApp/Hostel.ConsoleApp.csproj -c Release -o /app

# Stage 2: Runtime (much smaller image)
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Hostel.ConsoleApp.dll"]
```

**Benefits of multi-stage build:**
- Build image: ~800MB (SDK + everything needed to compile)
- Final image: ~220MB (runtime only — no SDK overhead)

---

## 📖 Usage Guide

### Main Menu

```
╔═══════════════════════════════════════════════╗
║          HOSTEL MANAGEMENT SYSTEM             ║
║               MAIN MENU                       ║
╠═══════════════════════════════════════════════╣
║  [1]  📊 Dashboard & Analytics                ║
║  [2]  👨‍🎓 Student Management                   ║
║  [3]  🏠 Room Management                      ║
║  [4]  💰 Fee & Payment Management             ║
║  [5]  📋 Complaint Management                 ║
║  [6]  👥 Staff Management                     ║
║  [7]  🚪 Visitor Log                          ║
║  [8]  📅 Attendance Tracking                  ║
║  [9]  🍽️  Mess Menu Management                ║
║  [10] 📢 Notice Board                         ║
║  [11] 📁 Reports & Export                     ║
║  [12] 📝 Audit Log                            ║
║  [13] 🏖️  Leave Management                    ║
║  [14] 📦 Inventory Management                 ║
║  [15] 🔄 Student Check-In/Out                 ║
║  [16] ⚙️  Admin Settings                       ║
║  [?]  ❓ Help                                 ║
║  [0]  🚪 Exit System                          ║
╚═══════════════════════════════════════════════╝
```

### Typical Workflow

1. **Login** → Enter `admin` / `admin123`
2. **Dashboard** → View real-time statistics
3. **Room Mgmt** → Create rooms before adding students
4. **Student Mgmt** → Register students and assign rooms
5. **Fee Mgmt** → Record monthly payments
6. **Reports** → Export summary reports

---

## 🔒 Security

### Password Hashing

Passwords are hashed using **PBKDF2 with HMAC-SHA256**:
- **100,000 iterations** (NIST recommended)
- **32-byte random salt** per password
- **32-byte derived key**
- Stored as `base64(salt) + ":" + base64(hash)`

```csharp
// Example: How passwords are hashed
private static string HashPassword(string password)
{
    byte[] salt = RandomNumberGenerator.GetBytes(32);
    byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
        password, salt, 100_000, HashAlgorithmName.SHA256, 32);
    return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
}
```

### Login Lockout

- 3 failed attempts → account locked for 5 minutes
- Configurable via `appsettings.json`

### Session Timeout

- Auto-logout after 15 minutes of inactivity (configurable)
- All sessions logged in the audit trail

---

## 💾 Data & Persistence

All data is stored as **formatted JSON files** in the `data/` directory:

| File | Contents |
|---|---|
| `students.json` | All student records |
| `rooms.json` | Room definitions and occupancy |
| `bookings.json` | Room booking history |
| `payments.json` | Payment records and receipts |
| `fees.json` | Fee structures per room type |
| `complaints.json` | All complaint records |
| `staff.json` | Staff profiles |
| `visitors.json` | Visitor log |
| `attendance.json` | Daily attendance records |
| `mess_menu.json` | Weekly mess menu |
| `notices.json` | Notice board entries |
| `audit.json` | Complete audit trail |
| `admins.json` | Admin accounts (passwords hashed) |
| `leaves.json` | Leave requests |
| `checkinout.json` | Student check-in/out log |
| `inventory.json` | Inventory items |
| `notifications.json` | System notifications |

### Backup & Restore

```
Admin Settings → Backup Data   → Creates /backups/backup_YYYYMMDD_HHmmss/
Admin Settings → Restore Backup → Choose from list and overwrite current data
```

---

## 🧪 Testing

The `Hostel.Tests` project contains **16 unit tests** using xUnit, covering:

- Student registration and room assignment
- Payment recording and receipt generation
- Complaint creation and status updates
- Room capacity enforcement
- Attendance statistics calculation
- Fee structure calculations

### Run Tests

```bash
# Run all tests
dotnet test

# Run with verbose output
dotnet test --logger "console;verbosity=detailed"
```

**All 16 tests pass ✅**

---

## 🎨 Design Patterns

| Pattern | Where Used |
|---|---|
| **Repository Pattern** | `IGenericRepository<T>` — `JsonFileRepository<T>` |
| **Service Layer** | All business logic in `*Service` classes |
| **Interface Segregation** | Separate interface per service domain |
| **Dependency Injection** | All services injected via constructor in `Program.cs` |
| **Partial Classes** | `HostelApp` split across multiple `.cs` files |
| **Async/Await** | All I/O and service calls are async |
| **Soft Delete** | `IsActive` flag used instead of hard-delete |
| **Factory Method** | `AppConfig.Load()` for configuration loading |
| **Observer (Audit)** | Every mutation logs to `AuditService` |
| **Strategy (Theme)** | `ThemeManager.ApplyTheme()` changes console colors |

---

## 📦 Dependencies

### Hostel.Core & Hostel.ConsoleApp
- **.NET 8 SDK** — No external NuGet packages required
- Uses only built-in: `System.Text.Json`, `System.Security.Cryptography`, `System.IO`

### Hostel.Tests
- **xUnit** — Test framework
- **xUnit.runner.visualstudio** — Visual Studio integration

---

## 🖥️ Configuration (`appsettings.json`)

The application loads its settings from `appsettings.json` at startup:

```json
{
  "HostelName": "National Hostel",
  "Version": "2.0.0",
  "DataDirectory": "data",
  "BackupDirectory": "backups",
  "ExportDirectory": "exports",
  "MaxLoginAttempts": 3,
  "LoginLockoutMinutes": 5,
  "SessionTimeoutMinutes": 15,
  "LateFeePerDay": 50,
  "Theme": "Dark",
  "EncryptJsonFiles": false
}
```

---

## 📈 Version History

See [CHANGELOG.md](CHANGELOG.md) for full release notes.

| Version | Date | Highlights |
|---|---|---|
| **2.0.0** | 2026-03-01 | 50 professional improvements, security hardening, analytics, Docker |
| **1.0.0** | 2026-02-27 | Initial release — basic console app with 4 menu options |

---

## 🗺️ Roadmap (Future Improvements)

- [ ] Web dashboard (ASP.NET Core MVC)
- [ ] REST API layer for mobile app integration
- [ ] SMS/email notifications via external API
- [ ] Database backend (PostgreSQL/SQLite via EF Core)
- [ ] Multi-hostel support
- [ ] PDF report generation
- [ ] Chart visualization in console (Spectre.Console)

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**Muhammad Hanzala**  
GitHub: [@Muhammad-Hanzala103](https://github.com/Muhammad-Hanzala103)

---

<div align="center">

⭐ **If this project helped you, please give it a star!** ⭐

*Built with ❤️ using C# and .NET 8*

</div>
