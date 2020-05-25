using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualQueue.Models
{
    public class ProjectConfig
    {
        [Key]
        public int att_id { get; set; }
        
        public string att_key { get; set; }
        public string att_val { get; set; }
    }
}