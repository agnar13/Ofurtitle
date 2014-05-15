using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using verklega.Models;


namespace verklega.Models
{
    public interface ISubtitleRepository
    {
        IEnumerable<Subtitle> GetSubtitles();
        Subtitle GetSubtitleByID(int id);
        void InsertSubtitle(Subtitle subtitle);
        //void Update(Subtitle subtitle);
        void Remove(int id);
        //void SearchSub(string subTitle);
        void SaveChanges();
        void InsertSubtitleLine(SubtitleLine subtitleline);       
        void InsertLineTranslation(LineTranslation line);
        IEnumerable<Language> GetLanguages();
    }
}
