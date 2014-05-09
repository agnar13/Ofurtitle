using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace verklega.Models
{
    public class User : IdentityUser
    {
        
        public string Rank { get; set; }
    }
}