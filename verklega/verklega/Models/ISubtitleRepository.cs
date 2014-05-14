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
        void Insert(Subtitle subtitle);
        void Update(Subtitle subtitle);
        void Remove(int id);
        void SearchSub(string subTitle);
        void SaveChanges();

        void InsertSLine(SubtitleLine subtitleline);
        

        /*
        void Create(Subtitle subtitle);
        void Create([Bind(Include = "ID,U_ID,L_ID,Title,Category")] Subtitles subtitles);
        void Delete(int? id);
        void SaveChanges();*/

        
        //void Edit(Student student);
 
    }
}
