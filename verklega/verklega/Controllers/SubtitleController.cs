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
using System.Web.UI.WebControls;

namespace verklega.Controllers
{
    public class ParseResult : SubtitleLine
    {
        //Creates a list of LineTranslation which are used in parsing
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

        //Constructor
         public SubtitleController()
        {
            this.subRepo = new SubtitleRepository(new AppDataContext());
        }

        //Constructor
        public SubtitleController(ISubtitleRepository subRepo)
        {
            this.subRepo = subRepo;
        }

        // GET: /Subtitle/Index
        //Index takes in a string called searchString
        public ActionResult Index(string searchString)
        {
            // GetSubtitles returns a list of Subtitles and matches them with Languages
            var showsubtitles = from Title in subRepo.GetSubtitles()
                                join Lang in subRepo.GetLanguages() on Title.L_ID equals Lang.ID
                                select Title;
            if (!String.IsNullOrEmpty(searchString))
            {
                //Query that searches for searchString that the user has inserted into the search bar
                //The search string is formatted so its not case sensitive.
                showsubtitles = showsubtitles.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            //Outcome is sent to the view as a list of items for simpler representation of data
            return View(showsubtitles.ToList());
        }
        
        // GET: /Subtitle/Create
        public ActionResult Create()
        {
            //Lists Languages for later use in dropdown lists
            ViewBag.Languages = subRepo.GetLanguages().Select(lang => new SelectListItem() { Text = lang.TextLanguage, Value = lang.ID.ToString() }).ToList();
            return View();
        }

        // POST: /Subtitle/Create
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

        //Creates a countable list out of the content.
        private IEnumerable<ParseResult> Parse(string content)
        {
            //Creates a new linked array of strings where each string is one line.
            string[] segments = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<ParseResult> result = new List<ParseResult>();

            foreach (string segment in segments)
            {
                //Creates an array called segmentParts. Splits the contents of segment into four different lines.
                //Also makes sure that the result does not contain an empty string.
                //Puts the split segment into the created array "segmentParts"
                string[] segmentParts = segment.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                
                //Makes sure that the split content is not less than 3 lines.
                if (segmentParts.Length >= 3)
                {
                    //Creates the segmentResult that is an instance of ParseResult.
                    ParseResult segmentResult = new ParseResult();

                    //Number is the element from the model class LineTranslation.
                    segmentResult.Start = segmentParts[0];

                    //Duration is the element from the model class LineTranslation.
                    segmentResult.Duration = segmentParts[1];

                    //Creates a new strongly typed list
                    List<string> lines = new List<string>();
                    
                    for (int i = 2; i < segmentParts.Length; i++)
                    {
                        LineTranslation line = new LineTranslation();

                        //Puts segmentParts into line.
                        line.Text = segmentParts[i];

                        //Adds the lines with the built in function
                        segmentResult.Lines.Add(line);
                    }
                    result.Add(segmentResult);
                }
            }
            return result;
        }

        [HttpPost, Authorize]
        public ActionResult AddSubtitle(Subtitle subtitle, HttpPostedFileBase file)
        {
            string FileNameExtention = System.IO.Path.GetExtension(file.FileName);
            //Gets the filname extention
            if (FileNameExtention == ".srt") //if the filname extention is not ".srt" then it rederects to the creat view
            {
                //Checks to see if the file contains any data and that all textboxes are filled
                if (file != null && file.ContentLength > 0 && subtitle.Title != null && subtitle.Category != null)
                {
                    //Since the program uses asp.net users, we get the current username by calling
                    //the built in function of asp.net, GetUserID.
                    subtitle.U_ID = User.Identity.GetUserId();

                    //Adds a new instance of subtitle into the database then the function InsertSubtitle
                    //saves it into the database so SubtitleLine and LineTranslation can get its ID.
                    subRepo.InsertSubtitle(subtitle);

                    //Creates a buffer that is the same size as the file
                    byte[] buffer = new byte[file.ContentLength];

                    //Puts the data into the buffer
                    file.InputStream.Read(buffer, 0, file.ContentLength);

                    //Converts the data in the buffer to a string
                    string result = System.Text.Encoding.UTF8.GetString(buffer);

                    //Sends the data to parseResults
                    IEnumerable<ParseResult> parseResults = Parse(result);

                    //Loops through the results and inserts them into the database tables.
                    foreach (ParseResult parseResult in parseResults)
                    {
                        //Since many files have multiple lines, this inner foreach loop takes care of that
                        foreach (LineTranslation line in parseResult.Lines)
                        {
                            //Linking the tables together and inserting into the database tables
                            line.L_ID = subtitle.L_ID;
                            line.U_ID = User.Identity.GetUserId();
                            subRepo.InsertLineTranslation(line);
                        }

                        //Sends the data in parseResult into the table SubtitleLine
                        parseResult.S_ID = subtitle.ID;
                        subRepo.InsertSubtitleLine(parseResult);
                        subRepo.SaveChanges();
                    }
                }
                else
                {
                    //This is only called if the file is empty
                    return RedirectToAction("Create");
                }

                return RedirectToAction("Index");
                //Sends the content into the parser in ViewSubtitle.
            }
            return RedirectToAction("Create");
        }
        
        //Links the id of the subtitle to a certain View
        //Used when the name of the subtitle is pushed in the search results
        public ActionResult ViewSubtitle(int id)
        {
            var ShowResult = subRepo.GetSubtitleByID(id);
            return View(ShowResult);
        }

        //Functions to download the subtitles by their id
        public FileContentResult GetFile(int id)
        {
            //Gets the subtitle by its id
            Subtitle subtitle = subRepo.GetSubtitleByID(id);
            
            //Puts both SubtitleLine and LineTranslation and links them to their individual id's
            IEnumerable<SubtitleLine> lines = subRepo.GetSubtitleLines(id);
            IEnumerable<LineTranslation> translations = subRepo.GetLineTranslations(id);

            //Sets KeyValue pair based on SL_ID
            ILookup<int,LineTranslation> translationsByLines = translations.ToLookup(trans => trans.SL_ID);

            //Initiate a new instance of StringBuilder
            StringBuilder sb = new StringBuilder();

            //Counter is used to increment the numbers in the chapters of the created .srt file
            int counter = 1;
            foreach (SubtitleLine line in lines)
            {
                //Insert lines into the textfile
                sb.AppendLine(counter.ToString());
                sb.AppendLine(line.Duration);
                
                //Gets the subtitle lines
                foreach (LineTranslation trans in translationsByLines[line.ID])
                {
                    sb.AppendLine(trans.Text);
                }

                //The .srt file format needs 1 empty line after each section
                sb.AppendLine();

                counter++;
            }

            //Default utf encoding for the .srt file
            byte[] data = System.Text.Encoding.Default.GetBytes(sb.ToString());

            //Returns a file in plain text that has an .srt ending
            return File(data, "text/plain", subtitle.Title + ".srt");
        }

    }
}
