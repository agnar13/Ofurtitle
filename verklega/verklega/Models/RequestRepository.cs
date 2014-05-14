using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using verklega.Models;

namespace verklega.Models
{
    public class RequestRepository : IRequestRepository
    {
        private AppDataContext context;

        public RequestRepository(AppDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Request> GetRequests()
        {
            return context.Requests.ToList();
        }

        public IEnumerable<Subtitle> GetTitle()
        {
            return context.Subtitles.ToList();
        }

        public IEnumerable<Language> GetLanguage()
        {
            return context.Languages.ToList();
        }
        
        public void Insert(Request request)
        {
            context.Requests.Add(request);
            context.SaveChanges();
        }
    }
}