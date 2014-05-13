using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace verklega.Models
{
    public class AppRepository : IAppRepository
    {
        private AppDataContext m_db = new AppDataContext();

        public IEnumerable<Language> GetLanguages()
        {
            return m_db.Languages;
        }

        // added
        public void Add(Language language)
        {
            m_db.Languages.Add(language);
            m_db.SaveChanges();
        }


        public void Remove(Language language)
        {
            m_db.Languages.Remove(language);
            m_db.SaveChanges();
        }


        public IEnumerable<Language> Find(Func<Language, bool> predicate)
        {
            return m_db.Languages.Where(predicate);
        }
    }
}