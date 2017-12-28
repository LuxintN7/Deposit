using Microsoft.AspNetCore.Mvc;

namespace DepositCore.Controllers
{
    public class PrinciplesController : Controller
    {
        // GET: Principles/Reliability
        public ActionResult Reliability()
        {
            return View();
        }

        // GET: Principles/Convenience
        public ActionResult Convenience()
        {
            return View();
        }

        // GET: Principles/Profitability
        public ActionResult Profitability()
        {
            return View();
        }
    }
}