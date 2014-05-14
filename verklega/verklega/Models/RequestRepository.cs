using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using verklega.Models;

namespace verklega.Models
{
    public class RequestRepository : IRequestRepository
    {
        // setup a connection to the database through AppDataContext
        private AppDataContext context;

        public RequestRepository(AppDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Request> GetRequests()
        {
            return context.Requests.ToList();
        }

        public void Insert(Request request)
        {
            // Add a new Request object to the Requests table
            context.Requests.Add(request);
            context.SaveChanges();
        }

    }
}