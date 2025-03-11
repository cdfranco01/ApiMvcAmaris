using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AmarisApi.Models;
using AmarisApi.DAL;

namespace AmarisApi.Controllers;

public class HomeController : Controller
{
    private readonly EmployeeApiService _service;
    public HomeController(EmployeeApiService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int? id)
    {
        try
        {
            var employees = id.HasValue ? new List<Employee> { await _service.GetEmployeeByIdAsync(id.Value) } : await _service.GetEmployeesAsync();
            return View(employees);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(new List<Employee>());
        }
    }
}
