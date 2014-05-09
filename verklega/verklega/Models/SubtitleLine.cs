using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace verklega.Models
{
    public class SubtitleLine
    {
        public int ID { get; set; }
        public int S_ID { get; set; }
        [ForeignKey("S_ID")]
        public virtual Subtitle Subtitle { get; set; }
        public string Start { get; set; }
        public string Duration { get; set; }
        public string SLText { get; set; }
    }
}