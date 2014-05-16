using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using verklega.Models;

namespace verklega.Controllers
{
    public class HomeController : Controller
    {
        private IAppRepository m_repository = null;

        public HomeController()
        {
            m_repository = new AppRepository();
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}