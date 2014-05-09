using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace verklega.Controllers
{
    public class SubtitleController : Controller
    {
        //
        // GET: /Subtitle/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSubtitle()
        {
            return View();
        }
        public ActionResult EditSubtitle()
        {
            return View();
        }
        public ActionResult SearchSubtitle()
        {
            return SearchSubtitle();
        }
        public ActionResult ViewSubtitle()
        {
            return ViewSubtitle();
        }
	}
}