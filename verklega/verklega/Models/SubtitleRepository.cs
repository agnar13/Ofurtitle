using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using verklega.Models;

namespace verklega.Models
{
    public class SubtitleRepository : ISubtitleRepository
    {
        //private AppDataContext s_db = new AppDataContext();
        private AppDataContext context;

        public SubtitleRepository(AppDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Subtitle> GetSubtitles()
        {
            return context.Subtitles.ToList();
        }

        public Subtitle GetSubtitleByID(int id)
        {
            return context.Subtitles.Find(id);
        }

        public void Insert(Subtitle subtitle)
        {
            // Add a new subtitle object to the Subtitles table
            context.Subtitles.Add(subtitle);
            //context.SaveChanges();
        }

        public void Remove(int id)
        {
            
            Subtitle sub = context.Subtitles.Find(id);
            context.Subtitles.Remove(sub);
            //context.SaveChanges();
        }

        public void Update(Subtitle subtitle)
        {
            //context.Entry(subtitle).State = 
            
        }

        public void SearchSub(string subTitle)
        {
            
        }

        public void InsertSLine(SubtitleLine subtitleline)
        {
            context.SubtitleLines.Add(subtitleline);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}