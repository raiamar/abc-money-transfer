using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
