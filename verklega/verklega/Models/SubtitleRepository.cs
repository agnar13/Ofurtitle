﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace verklega.Models
{
    public class SubtitleRepository : ISubtitleRepository, IDisposable
    {
        //private AppDataContext s_db = new AppDataContext();
        private AppDataContext context;

        public SubtitleRepository(AppDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Subtitle> GetSubtitles()
        {
            //return s_db.Subtitles;
            return context.Subtitles.ToList();
        }

        public Subtitle GetSubtitleByID(int id)
        {
            return context.Subtitles.Find(id);
        }

        public void Insert(Subtitle subtitle)
        {
            //s_db.Subtitles.Add(subtitle);
            //s_db.SaveChanges();
            context.Subtitles.Add(subtitle);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            //s_db.Subtitles.Remove(subtitle);
            //s_db.SaveChanges();
            Subtitle sub = context.Subtitles.Find(id);
            context.Subtitles.Remove(sub);
        }

        /*public void Edit(Subtitle subtitle)
        {
            context.Entry(subtitle).State = 
        }*/
    }
}