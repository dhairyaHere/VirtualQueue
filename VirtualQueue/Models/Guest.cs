using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualQueue.Models
{
    public class Guest
    {
        [Key]
        public long bookingID { get; set; }
        [Required]
        [Display(Name ="Guest Name")]
        public string guestName { get; set; }
        [EmailAddress]
        
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Contact No.")]
        [RegularExpression(@"^[0-9]{10}$",ErrorMessage ="Contact No. must be a valid 10-digit number!")]
        public string contact_no { get; set; }
        [Display(Name = "Group Size")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Group Size should be atleast 1")]
        public int groupSize { get; set; }
        [Display(Name = "Opt for mailing list")]
        public bool persist { get; set; } = false;
        [Display(Name = "V.I.P.")]
        public bool isVIP { get; set; } = false;
        public string status { get; set; }
        public DateTime entry { get; set; }
        public DateTime waiting { get; set; }
        public DateTime pending { get; set; }

    }
}