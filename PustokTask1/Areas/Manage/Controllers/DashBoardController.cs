using Microsoft.AspNetCore.Mvc;

namespace PustokTask1.Areas.Manage.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
