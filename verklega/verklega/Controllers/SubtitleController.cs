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

    public class ParseResult : SubtitleLine
    {
        public List<LineTranslation> Lines { get; set; }
    }

    public class SubtitleController : Controller
    {
        
        // Controller talks to Interface. Interface talks to Repository. Repository talks to database.
        // an instance of subRepo is declared??
        private ISubtitleRepository subRepo;

         public SubtitleController()
        {

            this.subRepo = new SubtitleRepository(new AppDataContext());
        }

         public SubtitleController(ISubtitleRepository subRepo)
         {
             this.subRepo = subRepo;
         }


        // GET: /Subtitle/
        public ActionResult Index()
        {
                                         // GetSubtitles returns a list of Subtitles
            var showsubtitles = from Title in subRepo.GetSubtitles()
                            select Title;
            // the list is sent to view
            return View(showsubtitles.ToList());
            //return View();
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
        public ActionResult Create([Bind(Include="ID,U_ID,L_ID,Title,Category")] Subtitle subtitle)
        {
            if (ModelState.IsValid)
            {
                subRepo.Insert(subtitle);
                //subRepo.SaveChanges();
                return RedirectToAction("Index");
            }

           /*ViewBag.L_ID = new SelectList(subRepo.Languages, "ID", "TextLanguage", subtitles.L_ID);
            ViewBag.U_ID = new SelectList(subRepo.Users, "ID", "Name", subtitles.U_ID);*/
            return View(subtitle);
        }

        
        // GET: /Subtitle/Edit/5
        public ActionResult Edit(int? id)
        {

            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            //Subtitle subtitle = subRepo.Subtitles.Find(id);
            /*if (subRepo.GetSubtitleByID(id) == null)
            {
                return HttpNotFound();
            }*/
            /*ViewBag.L_ID = new SelectList(db.Languages, "ID", "TextLanguage", subtitles.L_ID);
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Name", subtitles.U_ID);*/
            return View();
        }

        // POST: /Subtitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
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
        

        /*
        // GET: /Subtitle/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtitle subtitle = subRepo.GetSubtitleByID(id);
            
            if (subtitle == null)
            {
                return HttpNotFound();
            }
            return View(subtitle);
        }
         */


        /*
        // POST: /Subtitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
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
        private IEnumerable<ParseResult> Parse(string content)
        {
            string[] segments = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<ParseResult> result = new List<ParseResult>();

            foreach (string segment in segments)
            {
                string[] segmentParts = segment.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (segmentParts.Length >= 3)
                {
                    ParseResult segmentResult = new ParseResult();

                    //result.Number = segmentParts[0];
                    //result.Start/Stop = segmentParts[1];

                    List<string> lines = new List<string>();
                    for (int i = 2; i < segmentParts.Length; i++)
                    {
                        LineTranslation line = new LineTranslation();
                        line.Text = segmentParts[i];
                        segmentResult.Lines.Add(line);
                    }


                    result.Add(segmentResult);
                }

            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSub(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                //file.SaveAs(path);
                byte[] buffer = new byte[file.ContentLength];
                file.InputStream.Read(buffer, 0, file.ContentLength);
                string result = System.Text.Encoding.UTF8.GetString(buffer);

                Parse(result);
            }

            return RedirectToAction("ViewSubtitle");
        }

        public ActionResult SearchSubtitle()
        {
            /*var showsubtitles = from Title in subRepo.GetSubtitles()
                                select Title;
            return View(showsubtitles.ToList());*/
            return View();
        }
        public ActionResult ViewSubtitle()
        {
            return View();
        }

    }
}
