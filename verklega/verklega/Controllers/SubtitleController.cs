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
    public class SubtitleController : Controller
    {
        //private VERK014_H36Entities db = new VERK014_H36Entities();
         //private ISubtitleRepository m_repository = null;
        
        private ISubtitleRepository subRepo;

         public SubtitleController()
        {
           // m_repository = new Repository();
            //this.db = new SubtitleRepository(new AppDataContext);

            this.subRepo = new SubtitleRepository(new AppDataContext());
        }

         public SubtitleController(ISubtitleRepository subRepo)
         {
             this.subRepo = subRepo;
         }


        // GET: /Subtitle/
        public ActionResult Index()
        {
            //var subtitles = db.Subtitles.Include(s => s.Languages).Include(s => s.Users);
            var subtitles = from Title in subRepo.GetSubtitles()
                            select Title;
            return View(subtitles.ToList());
        }


        /*
        // GET: /Subtitle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtitles subtitles = db.Subtitles.Find(id);
            if (subtitles == null)
            {
                return HttpNotFound();
            }
            return View(subtitles);
        }*/

        
        // GET: /Subtitle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Subtitle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,U_ID,L_ID,Title,Category")] Subtitles subtitles)
        {
            if (ModelState.IsValid)
            {
                subRepo.Insert(subtitles);
                //subRepo.SaveChanges();
                return RedirectToAction("Index");
            }

           /* ViewBag.L_ID = new SelectList(subRepo.Languages, "ID", "TextLanguage", subtitles.L_ID);
            ViewBag.U_ID = new SelectList(subRepo.Users, "ID", "Name", subtitles.U_ID);*/
            return View(subtitles);
        }

        /*
        // GET: /Subtitle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtitles subtitles = db.Subtitles.Find(id);
            if (subtitles == null)
            {
                return HttpNotFound();
            }
            ViewBag.L_ID = new SelectList(db.Languages, "ID", "TextLanguage", subtitles.L_ID);
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Name", subtitles.U_ID);
            return View(subtitles);
        }

        // POST: /Subtitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,U_ID,L_ID,Title,Category")] Subtitles subtitles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subtitles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.L_ID = new SelectList(db.Languages, "ID", "TextLanguage", subtitles.L_ID);
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Name", subtitles.U_ID);
            return View(subtitles);
        }
        */
        


        // GET: /Subtitle/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Subtitles subtitles = db.Subtitles.Find(id);
            Subtitles subtitles = subRepo.GetSubtitleByID(id);
            
            if (subtitles == null)
            {
                return HttpNotFound();
            }
            return View(subtitles);
        }


        /*
        // POST: /Subtitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subtitles subtitles = db.Subtitles.Find(id);
            db.Subtitles.Remove(subtitles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
       
    }
}
