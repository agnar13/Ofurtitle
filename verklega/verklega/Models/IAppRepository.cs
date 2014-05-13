using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using verklega.Models;

namespace verklega.Models
{
    interface IAppRepository
    {
        IEnumerable<Language> GetLanguages();
        void Add(Language TextLanguage);
        void Remove(Language TextLanguage);
        IEnumerable<Language> Find(Func<Language, bool> predicate);

        
    }
}
