using Microsoft.AspNetCore.Mvc;

namespace bank_data_web_application.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
