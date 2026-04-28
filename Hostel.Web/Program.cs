using Hostel.Core.Interfaces;
using Hostel.Core.Entities;
using Hostel.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var config = AppConfig.Load();
var dataDir = Path.Combine(Directory.GetCurrentDirectory(), config.DataDirectory);
builder.Services.AddSingleton(config);

builder.Services.AddSingleton<IGenericRepository<Student>>(_ => new JsonFileRepository<Student>(dataDir, "students.json"));
builder.Services.AddSingleton<IGenericRepository<Room>>(_ => new JsonFileRepository<Room>(dataDir, "rooms.json"));
builder.Services.AddSingleton<IGenericRepository<Booking>>(_ => new JsonFileRepository<Booking>(dataDir, "bookings.json"));
builder.Services.AddSingleton<IGenericRepository<Payment>>(_ => new JsonFileRepository<Payment>(dataDir, "payments.json"));
builder.Services.AddSingleton<IGenericRepository<Complaint>>(_ => new JsonFileRepository<Complaint>(dataDir, "complaints.json"));
builder.Services.AddSingleton<IGenericRepository<Staff>>(_ => new JsonFileRepository<Staff>(dataDir, "staff.json"));

builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IRoomService, RoomService>();
builder.Services.AddSingleton<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IComplaintService, ComplaintService>();
builder.Services.AddSingleton<IStaffService, StaffService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

