using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using verklega.Models;
using Microsoft.AspNet.Identity;
using System.Text;

namespace verklega.Controllers
{
    public class ParseResult : SubtitleLine
    {
        public List<LineTranslation> Lines { get; set; }
        public ParseResult()
        {
            Lines = new List<LineTranslation>();
        }
    }

    public class SubtitleController : Controller
    {
        // An instance of the interface ISubtitleRepository is declared.
        // The interface then talks to the repository which in turn talks to the database.
        private ISubtitleRepository subRepo;

        //**Constructors
         public SubtitleController()
        {
            this.subRepo = new SubtitleRepository(new AppDataContext());
        }

         public SubtitleController(ISubtitleRepository subRepo)
         {
             this.subRepo = subRepo;
         }
        //**

        // GET: /Subtitle/
         public ActionResult Index(string searchString)
         {
             // GetSubtitles returns a list of Subtitles
             var showsubtitles = from Title in subRepo.GetSubtitles()
                                 join Lang in subRepo.GetLanguages() on Title.L_ID equals Lang.ID
                                 select Title;
             //join Lang in subRepo.GetLanguages() on Title.L_ID equals Lang.ID
             //select new { Title.Title, Lang.TextLanguage };
             //select Title;
             if (!String.IsNullOrEmpty(searchString))
             {   // searches the list looking for matches with the search string
                 //showsubtitles = showsubtitles.Where(s => s.Title.Contains(searchString));
                 showsubtitles = showsubtitles.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
             }
             // and outcome is sent to the view
             return View(showsubtitles.ToList());
         }
        
        // GET: /Subtitle/Create
        public ActionResult Create()
        {
            ViewBag.Languages = subRepo.GetLanguages().Select(lang => new SelectListItem() { Text = lang.TextLanguage, Value = lang.ID.ToString() }).ToList();
            ViewBag.Category = subRepo.GetSubtitles().Select(Cat => new SelectListItem() { Text = Cat.Category, Value = Cat.ID.ToString() }).ToList();
            return View();
        }

        // POST: /Subtitle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,U_ID,L_ID,Title,Category")] Subtitle subtitle)
        {
            if (ModelState.IsValid)
            {
                subRepo.InsertSubtitle(subtitle);
                subRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subtitle);
        }

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
        [HttpPost, Authorize]
        public ActionResult AddSubtitle(Subtitle subtitle, HttpPostedFileBase file)
        {
            subtitle.U_ID = User.Identity.GetUserId();

            subRepo.InsertSubtitle(subtitle);

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
                IEnumerable<ParseResult> parseResults = Parse(result);
                //Sendir gögnin í parse result.

                foreach (ParseResult parseResult in parseResults)
                {
                    foreach (LineTranslation line in parseResult.Lines)
                    {
                        line.L_ID = subtitle.L_ID;
                        line.U_ID = User.Identity.GetUserId();

                        subRepo.InsertLineTranslation(line);
                    }

                    parseResult.S_ID = subtitle.ID;
                    subRepo.InsertSubtitleLine(parseResult);
                    subRepo.SaveChanges();
                }
            }
           
            return RedirectToAction("ViewSubtitle");
            //Sends the content into the parser in ViewSubtitle.
        }



        public ActionResult SearchSubtitle()
        {
           return View();
        }
        public ActionResult ViewSubtitle(int id)
        {
            var ShowResult = subRepo.GetSubtitleByID(id);
            return View(ShowResult);
        }

        public FileContentResult GetFile(int id)        
        {
            Subtitle subtitle = subRepo.GetSubtitleByID(id);
            
            IEnumerable<SubtitleLine> lines = subRepo.GetSubtitleLines(id);
            IEnumerable<LineTranslation> translations = subRepo.GetLineTranslations(id);

            ILookup<int,LineTranslation> translationsByLines = translations.ToLookup(trans => trans.SL_ID);

            StringBuilder sb = new StringBuilder();

            int counter = 1;
            foreach (SubtitleLine line in lines)
            {
                sb.AppendLine(counter.ToString());
                sb.AppendLine(line.Duration);
                foreach (LineTranslation trans in translationsByLines[line.ID])
                {
                    sb.AppendLine(trans.Text);
                }

                sb.AppendLine();

                counter++;
            }

            byte[] data = System.Text.Encoding.Default.GetBytes(sb.ToString());

            return File(data, "text/plain", subtitle.Title + ".srt");
        }

    }
}
