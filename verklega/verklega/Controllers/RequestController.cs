﻿using System;
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

        // Constructors
        public RequestController()
        {
            this.reqRepo = new RequestRepository(new AppDataContext());
        }

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
            reqRepo.Insert(request);
            reqRepo.SaveChanges();
        }
	}
}