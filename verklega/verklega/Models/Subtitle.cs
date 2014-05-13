using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace verklega.Models
{
    public class Subtitle
    {

        public int ID { get; set; }
        public string U_ID { get; set; }
        [ForeignKey("U_ID")]
        public virtual User User { get; set; }
        //public User User { get; set; }
        public int L_ID { get; set; }
        [ForeignKey("L_ID")]
        public virtual Language Language { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }

    }

}