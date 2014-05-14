using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using verklega.Models;

namespace verklega.Models
{
    public interface IRequestRepository
    {
        IEnumerable<Request> GetRequests();
        IEnumerable<Subtitle> GetTitle();
        IEnumerable<Language> GetLanguage();
        void Insert(Request request);
    }
}