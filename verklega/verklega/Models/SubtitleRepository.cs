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
        // AppDataContext connects to the database
        private AppDataContext context;

        // Constructor
        public SubtitleRepository(AppDataContext context)
        {
            this.context = context;
        }

        // A function that return a list of Subtitles
        public IEnumerable<Subtitle> GetSubtitles()
        {
            return context.Subtitles.ToList();
        }

        // Returns an id from the table Subtitles
        public Subtitle GetSubtitleByID(int id)
        {
            return context.Subtitles.Find(id);
        }

        // Adds a new object of Subtitles to the database
        public void InsertSubtitle(Subtitle subtitle)
        {
            // Add a new subtitle object to the Subtitles table
            context.Subtitles.Add(subtitle);
            context.SaveChanges();
        }

        // Remove an object of Subtitles from the database
        public void Remove(int id)
        {
            Subtitle sub = context.Subtitles.Find(id);
            context.Subtitles.Remove(sub);
        }

        // Adds an object of SubtitleLine to the database
        public void InsertSubtitleLine(SubtitleLine subtitleline)
        {
            context.SubtitleLines.Add(subtitleline);
        }

        // A function that adds an object of LineTranslation to the database
        public void InsertLineTranslation(LineTranslation line)
        {
            context.LineTranslations.Add(line);
        }

        //public void UpdateSubtitleLine()
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        // Returns a list of Languages
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

        public IEnumerable<Subtitle> GetCategory()
        {
            return context.Subtitles.ToList();
        }
    }
}