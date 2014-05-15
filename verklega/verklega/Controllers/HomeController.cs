using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using verklega.Models;

namespace verklega.Controllers
{
    public class HomeController : Controller
    {
        private IAppRepository m_repository = null;

        public HomeController()
        {
            m_repository = new AppRepository();
        }


        public ActionResult Index()
        {
            
            //AppDataContext dba = new AppDataContext();

            /*Language t = new Language { TextLanguage = "Tyrkneska"};
            dba.Languages.Add(t);
            dba.SaveChanges();
             * 
             * 
             * 
            var langs = from TextLanguage in dba.Languages
                        select TextLanguage;*/

            /*Language t = new Language { TextLanguage = "Tyrkneska" };
            m_db.Languages.Add(t);
            m_repository.Languages.SaveChanges();*/
  
            /*Language t = new Language { TextLanguage = "Kóreska" };
            m_repository.Add(t);
            m_repository.SaveChanges();*/

            //Language bask = new Language { TextLanguage = "Basknenska" };
           // m_repository.Remove(bask);
            
            //var langs = from TextLanguage in m_repository.GetLanguages()
                       // select TextLanguage;

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            ViewBag.Message = "Insert into languages and show list";

            /*
            Language italian = new Language { TextLanguage = "Italian" };
            var finnska = new Language { TextLanguage = "Finnska"};
            var danska = new Language { TextLanguage = "Danska" };
            */
            

            //var appRep = new AppRepository();

            // add
            //m_repository.Add(italian);
            //m_repository.Add(danska);
          
            //m_repository.SaveChanges();
            //appRep.Add(finnska);
  
            // update
            // Language arabic = m_repository.Find(p => Id == 3).Single();
            // arabic.TextLanguage(arabic);

            //remove
           //m_repository.Remove(bask);

            /*foreach (Language slang in m_repository.Find(p => p.ID > 0))
            {
                Console.WriteLine("Name:{0}", slang.TextLanguage);
            }

            var dangs = from TextLanguage in m_repository.GetLanguages()
                        select TextLanguage;

            return View(dangs);*/

            return View();
        }
        /*
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Message = "bla";

            return View();
        }*/
        
        public ActionResult Contact(string searchString) 
        {           
            var movies = from m in m_repository.GetLanguages()
                         select m; 
 
            if (!String.IsNullOrEmpty(searchString)) 
            { 
                movies = movies.Where(s => s.TextLanguage.Contains(searchString)); 
            } 
 
            return View(movies); 
        }
          
          


    }
}