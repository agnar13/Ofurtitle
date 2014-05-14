using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using verklega.Models;

namespace verklega.Controllers
{
    public class RequestController : Controller
    {

        private IRequestRepository reqRepo;
        //
        // GET: /Request/

        /*
        var query = from ot in context.OTs
                join v in context.Vehicles on ot.vehicle_id equals v.id
                join c in context.Clients on v.client_id equals c.id
                where c.Name == clientName
                select new {ot, c};
        /*
        var beverages =
            (from products in GetProducts()
             join categories in GetCategories() on products.CategoryID equals categories.ID
             where products.Name.StartsWith("G")
             where categories.Name == "Beverages"
             orderby products.Name
             select products
            );
         */

        public RequestController()
        {
            this.reqRepo = new RequestRepository(new AppDataContext());
        }

        public RequestController(IRequestRepository reqRepo)
        {
            this.reqRepo = reqRepo;
        }

        public ActionResult Index()
        {
            var showRequests = (from Req in reqRepo.GetRequests()
                                join Title in reqRepo.GetTitle() on Req.S_ID equals Title.ID
                                join Lang in reqRepo.GetLanguage() on Title.L_ID equals Lang.ID
                                where Title.ID == Req.S_ID
                                select Req).ToList();
            return View(showRequests);
        }

        public ActionResult ViewRequest()
        {
            return View();
        }

        public void Create(Request request)
        {
            reqRepo.Insert(request);
        }
	}
}