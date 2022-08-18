using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
