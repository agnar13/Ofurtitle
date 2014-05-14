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
         //private IRequestRepository reqRepo = null;
        private IRequestRepository reqRepo;

         public RequestController()
        {

            this.reqRepo = new RequestRepository(new AppDataContext());
        }

         public RequestController(IRequestRepository reqRepo)
         {
             this.reqRepo = reqRepo;
         }

        //
        // GET: /Request/
        public ActionResult Index()
        {                       // GetRequests returns a list of Requests
           /* var showRequests = (from Req in reqRepo.GetRequests()
                                join Title in reqRepo.GetTitle() on Req.S_ID equals Title.ID
                                join Lang in reqRepo.GetLanguages() on Title.L_ID equals Lang.ID
                                where Title.ID == Req.S_ID
                                select Req).ToList();
            return View(showRequests);*/

            
            var ShowRequest = from ID in reqRepo.GetRequests()
                                select ID;
            return View(ShowRequest.ToList());
        }

        public ActionResult ViewRequest(List<string> listofString)
        {
            return View(listofString);
        }

        public void Create(Request request)
        {
            reqRepo.Insert(request);
        }
	}
}