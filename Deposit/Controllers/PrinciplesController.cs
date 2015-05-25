using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deposit.Controllers
{
    public class PrinciplesController : Controller
    {
        // GET: Principles
        public ActionResult Index()
        {
            return View();
        }

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