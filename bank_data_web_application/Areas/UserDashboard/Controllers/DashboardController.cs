using Microsoft.AspNetCore.Mvc;

namespace bank_data_web_application.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
