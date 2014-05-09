using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace verklega.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SubtitleLine> SubtitleLines { get; set; }
        public DbSet<LineTranslation> LineTranslations { get; set; }

        
     protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
    }
    }

}