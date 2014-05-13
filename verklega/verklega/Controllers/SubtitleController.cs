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
            var showsubtitles = from Title in subRepo.GetSubtitles()
                            select Title;
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
        //Creates a countable list out of the content.
        {
            string[] segments = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //Creates a new linked array of strings where each string is one line.
            List<ParseResult> result = new List<ParseResult>();
            //
            foreach (string segment in segments)
            {
                string[] segmentParts = segment.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //Creates an array called segmentParts. Plits the contents of segment into four different lines.
                //Also makes sure that the result does not contain an empty string.
                //Puts the split segment into the created array "segmentParts"
                if (segmentParts.Length >= 3)
                //Makes sure that the split content is not less than 3 lines.
                {
                    ParseResult segmentResult = new ParseResult();
                    //Creates the segmentResult that is an instance of ParseResult.

                    segmentResult.Start = segmentParts[0];
                    //segmentPart[0] is an instance of the number of the content.
                    //Number is the element from the model class LineTranslation.
                    segmentResult.Duration = segmentParts[1];
                    //segmentPart[1] is an instance of The duration of the Content.
                    //Duration is the element from the model class LineTranslation.
                    List<string> lines = new List<string>();
                    //Creates a new strongly typed list

                    for (int i = 2; i < segmentParts.Length; i++)
                    {
                        LineTranslation line = new LineTranslation();
                        line.Text = segmentParts[i];
                        //Puts segmentParts into line.
                        segmentResult.Lines.Add(line);
                        //????
                    }

                    result.Add(segmentResult);
                }

            }

            return result;
        }

        [HttpPost]
        public ActionResult AddSubtitle(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            //Ef skráinn inniheldur einhver gögn.
            {
                //file.SaveAs(path); ( aðferð til að vista gögn locally)
                byte[] buffer = new byte[file.ContentLength];
                //Býr til buffer sem er jafn stór og skráin.
                file.InputStream.Read(buffer, 0, file.ContentLength);
                //Setur gögnin inn í buffer.
                string result = System.Text.Encoding.UTF8.GetString(buffer);
                //Breytir gögnunum í buffernum í streng.

                Parse(result);
                //Sendir gögnin í parse result.
            }

            return RedirectToAction("ViewSubtitle");
            //Sends the content into the parser in ViewSubtitle.
        }

        public ActionResult SearchSubtitle()
        {
            return View();
        }
        public ActionResult ViewSubtitle()
        {
            return View();
        }

    }
}
