using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace verklega.Models
{
    public class LineTranslation
    {
        public int ID { get; set; }
        public int SL_ID { get; set; }
        [ForeignKey("SL_ID")]
        public virtual SubtitleLine SubtitleLine { get; set; }
        public int U_ID { get; set; }
        [ForeignKey("U_ID")]
        public virtual User User { get; set; }
        public int L_ID { get; set; }
        [ForeignKey("L_ID")]
        public virtual Language Language { get; set; }
        public string Text { get; set; }

    }
}