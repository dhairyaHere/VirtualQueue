using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualQueue.Models
{
    public class Credential
    {
        [Key]

        public int RoleID { get; set; }

        [Required]
        
        public string roleType { get; set; }
        [Required]
        public string password { get; set; }
    }
}