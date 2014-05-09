using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace verklega.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string U_ID { get; set; }
        [ForeignKey("U_ID")]
        public virtual User User { get; set; }
        public int S_ID { get; set; }
        [ForeignKey("S_ID")]
        public virtual Subtitle Subtitle { get; set; }
    }
}