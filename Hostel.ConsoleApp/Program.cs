using Microsoft.Extensions.DependencyInjection;
using Hostel.Core.Entities;
using Hostel.Core.Interfaces;
using Hostel.Core.Services;
using Hostel.ConsoleApp;

// ═══════════════════════════════════════════════════════════════
//  LOAD CONFIG (#13)
// ═══════════════════════════════════════════════════════════════
var config = AppConfig.Load();
FileLogger.Info("App", "Application starting...");

var workingDir = Directory.GetCurrentDirectory();
var dataDir = Path.Combine(workingDir, config.DataDirectory);
var backupDir = Path.Combine(workingDir, config.BackupDirectory);
var exportDir = Path.Combine(workingDir, config.ExportDirectory);

// ═══════════════════════════════════════════════════════════════
//  DEPENDENCY INJECTION — Service Collection
// ═══════════════════════════════════════════════════════════════
var services = new ServiceCollection();

// Register Config & Core Infrastructure
services.AddSingleton(config);
services.AddSingleton<LoginLockoutManager>(new LoginLockoutManager(config.MaxLoginAttempts, config.LoginLockoutMinutes));
services.AddSingleton<BackupService>(new BackupService(dataDir, backupDir));
services.AddSingleton<FormattedReportExporter>(new FormattedReportExporter(exportDir));

// Register Repositories (Singleton as they manage in-memory state)
services.AddSingleton<IGenericRepository<Student>>(new JsonFileRepository<Student>(dataDir, "students.json"));
services.AddSingleton<IGenericRepository<Room>>(new JsonFileRepository<Room>(dataDir, "rooms.json"));
services.AddSingleton<IGenericRepository<Booking>>(new JsonFileRepository<Booking>(dataDir, "bookings.json"));
services.AddSingleton<IGenericRepository<Payment>>(new JsonFileRepository<Payment>(dataDir, "payments.json"));
services.AddSingleton<IGenericRepository<FeeStructure>>(new JsonFileRepository<FeeStructure>(dataDir, "fees.json"));
services.AddSingleton<IGenericRepository<Complaint>>(new JsonFileRepository<Complaint>(dataDir, "complaints.json"));
services.AddSingleton<IGenericRepository<Staff>>(new JsonFileRepository<Staff>(dataDir, "staff.json"));
services.AddSingleton<IGenericRepository<Visitor>>(new JsonFileRepository<Visitor>(dataDir, "visitors.json"));
services.AddSingleton<IGenericRepository<Attendance>>(new JsonFileRepository<Attendance>(dataDir, "attendance.json"));
services.AddSingleton<IGenericRepository<MessMenu>>(new JsonFileRepository<MessMenu>(dataDir, "mess_menu.json"));
services.AddSingleton<IGenericRepository<Notice>>(new JsonFileRepository<Notice>(dataDir, "notices.json"));
services.AddSingleton<IGenericRepository<AuditLog>>(new JsonFileRepository<AuditLog>(dataDir, "audit.json"));
services.AddSingleton<IGenericRepository<Admin>>(new JsonFileRepository<Admin>(dataDir, "admins.json"));
services.AddSingleton<IGenericRepository<LeaveRequest>>(new JsonFileRepository<LeaveRequest>(dataDir, "leaves.json"));
services.AddSingleton<IGenericRepository<StudentCheckInOut>>(new JsonFileRepository<StudentCheckInOut>(dataDir, "checkinout.json"));
services.AddSingleton<IGenericRepository<InventoryItem>>(new JsonFileRepository<InventoryItem>(dataDir, "inventory.json"));
services.AddSingleton<IGenericRepository<Notification>>(new JsonFileRepository<Notification>(dataDir, "notifications.json"));

// Register Business Services
services.AddSingleton<IStudentService, StudentService>();
services.AddSingleton<IRoomService, RoomService>();
services.AddSingleton<IPaymentService, PaymentService>();
services.AddSingleton<IFeeStructureService, FeeStructureService>();
services.AddSingleton<IComplaintService, ComplaintService>();
services.AddSingleton<IStaffService, StaffService>();
services.AddSingleton<IVisitorService, VisitorService>();
services.AddSingleton<IAttendanceService, AttendanceService>();
services.AddSingleton<IMessMenuService, MessMenuService>();
services.AddSingleton<INoticeService, NoticeService>();
services.AddSingleton<IAuditService, AuditService>();
services.AddSingleton<IAdminService, AdminService>();

// Advanced services
services.AddSingleton<LeaveService>();
services.AddSingleton<StudentCheckInOutService>();
services.AddSingleton<InventoryService>();
services.AddSingleton<NotificationService>();
services.AddSingleton<TrendAnalyzer>();
services.AddSingleton<OccupancyForecaster>();
services.AddSingleton<RoomAllocationEngine>();

// Register Seeder & Main App
services.AddSingleton<DataSeeder>();
services.AddSingleton<HostelApp>();

// Build provider
var serviceProvider = services.BuildServiceProvider();

// ═══════════════════════════════════════════════════════════════
//  APP SETUP
// ═══════════════════════════════════════════════════════════════
Console.OutputEncoding = System.Text.Encoding.UTF8;
ThemeManager.ApplyTheme(config.Theme);

// ═══════════════════════════════════════════════════════════════
//  DATA SEEDING
// ═══════════════════════════════════════════════════════════════
var seeder = serviceProvider.GetRequiredService<DataSeeder>();

if (!await seeder.IsSeededAsync())
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n    ⏳ First run detected — seeding demo data...");
    Console.ResetColor();
    await seeder.SeedAsync();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("    ✅ Demo data loaded! (10 students, 15 rooms, 5 staff, menus & more)");
    Console.ResetColor();
    Console.WriteLine();
}

FileLogger.Info("App", "All services initialized successfully via DI container");

// ═══════════════════════════════════════════════════════════════
//  LAUNCH APP
// ═══════════════════════════════════════════════════════════════
var app = serviceProvider.GetRequiredService<HostelApp>();
await app.RunAsync();
