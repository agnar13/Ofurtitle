using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using verklega.Models;

namespace verklega.Models
{
    public class RequestRepository : IRequestRepository
    {
        // Setup a connection to the database through AppDataContext

        private AppDataContext context;

        // Constructor
        public RequestRepository(AppDataContext context)
        {
            this.context = context;
        }

        // A function that returns a list of requests
        public IEnumerable<Request> GetRequests()
        {
            return context.Requests.ToList();
        }

        // A function that returns a list of subtitles
        public IEnumerable<Subtitle> GetTitle()
        {
            return context.Subtitles.ToList();
        }

        // Returns a list of languages ffrom the database
        public IEnumerable<Language> GetLanguage()
        {
            return context.Languages.ToList();
        }
        
        public void Insert(Request request)
        {
            context.Requests.Add(request);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}