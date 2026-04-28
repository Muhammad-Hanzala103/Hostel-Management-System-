using Hostel.Core.Entities;
using Hostel.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IStudentService _students;
    private readonly IRoomService _rooms;
    private readonly IComplaintService _complaints;
    private readonly IPaymentService _payments;

    public HomeController(
        IStudentService students,
        IRoomService rooms,
        IComplaintService complaints,
        IPaymentService payments)
    {
        _students = students;
        _rooms = rooms;
        _complaints = complaints;
        _payments = payments;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _students.GetAllStudentsAsync();
        var rooms = await _rooms.GetAllRoomsAsync();
        var complaints = await _complaints.GetAllComplaintsAsync();
        var payments = await _payments.GetAllPaymentsAsync();

        var model = new DashboardViewModel
        {
            TotalStudents = students.Count,
            TotalRooms = rooms.Count,
            OccupiedRooms = rooms.Count(r => r.CurrentOccupancy > 0),
            OpenComplaints = complaints.Count(c => c.Status != ComplaintStatus.Resolved && c.Status != ComplaintStatus.Closed),
            TotalPayments = payments.Count,
            TotalRevenue = payments.Where(p => p.Status == PaymentStatus.Paid).Sum(p => p.Amount)
        };

        return View(model);
    }
}

public class DashboardViewModel
{
    public int TotalStudents { get; set; }
    public int TotalRooms { get; set; }
    public int OccupiedRooms { get; set; }
    public int OpenComplaints { get; set; }
    public int TotalPayments { get; set; }
    public decimal TotalRevenue { get; set; }
}

