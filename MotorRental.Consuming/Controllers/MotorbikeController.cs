using Microsoft.AspNetCore.Mvc;

namespace MotorRental.Consuming.Controllers
{
    public class MotorbikeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
