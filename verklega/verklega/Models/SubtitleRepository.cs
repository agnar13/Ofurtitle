using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public void InsertSubtitle(Subtitle subtitle)
        {
            // Add a new subtitle object to the Subtitles table

            context.Subtitles.Add(subtitle);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            Subtitle sub = context.Subtitles.Find(id);
            context.Subtitles.Remove(sub);
            //context.SaveChanges();
        }

        /*public void Update(Subtitle subtitle)
        {
            //context.Entry(subtitle).State = 
            
        }

        public void SearchSub(string subTitle)
        {
            
        }*/

        public void InsertSubtitleLine(SubtitleLine subtitleline)
        {
            context.SubtitleLines.Add(subtitleline);
        }

        public void InsertLineTranslation(LineTranslation line)
        {
            context.LineTranslations.Add(line);
        }

        //public void UpdateSubtitleLine()

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IEnumerable<Language> GetLanguages()
        {
            return context.Languages.ToList();
        }

        public IEnumerable<SubtitleLine> GetSubtitleLines(int subtitleId)
        {
            return context.SubtitleLines.Where(line => line.S_ID == subtitleId).ToList();
        }

        public IEnumerable<LineTranslation> GetLineTranslations(int subtitleId)
        {
            return (from line in context.SubtitleLines
                    join translation in context.LineTranslations on line.ID equals translation.SL_ID
                    select translation).ToList();
        }
    }
}