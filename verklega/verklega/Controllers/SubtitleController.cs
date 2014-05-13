using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return View();
        }
        public ActionResult ViewSubtitle()
        {
            return View();
        }

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
        public ActionResult AddSubtitle(HttpPostedFileBase file)
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
	}
}