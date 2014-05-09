using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace verklega.Models
{
    public class Language
    {
        public int ID { get; set; }
        [ForeignKey("ID")]
        public virtual Request Request { get; set; }
        public string TextLanguage { get; set; }
        
    }
}