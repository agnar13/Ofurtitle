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

        // Constructor
        public RequestController()
        {
            this.reqRepo = new RequestRepository(new AppDataContext());
        }

        //Constructor
        public RequestController(IRequestRepository reqRepo)
        {
            this.reqRepo = reqRepo;
        }

        public ActionResult Index(string InputString)
        {
            //Linq query to join the tables Requests, Subtitles and Languages where the ID matches.
            //Used to show the title of the Subtitle in the Request index.

            var showRequests = (from Req in reqRepo.GetRequests()
                                join Title in reqRepo.GetTitle() on Req.S_ID equals Title.ID
                                join Lang in reqRepo.GetLanguage() on Title.L_ID equals Lang.ID
                                where Title.ID == Req.S_ID
                                select Req);

            //Query that searches for InputString that the user has inserted into the search bar
            //The search string is formatted so its not case sensitive.
            if (!String.IsNullOrEmpty(InputString))
            {
                showRequests = showRequests.Where(s => s.Subtitle.Title.ToUpper().Contains(InputString.ToUpper()));
            }
            return View(showRequests.ToList());
        }

        public ActionResult ViewRequest()
        {
            return View();
        }

        public void CreateRequest(Request request)
        {
            //Uses the built in function to insert into the Request table
            reqRepo.Insert(request);
            reqRepo.SaveChanges();
        }
	}
}