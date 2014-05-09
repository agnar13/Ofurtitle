using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace verklega.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
<<<<<<< HEAD
=======
            xxxy
                meira af drasli muhahahah
>>>>>>> 71b8a254298c9fa6f25cc3381ae9b2e4405a7a94
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}