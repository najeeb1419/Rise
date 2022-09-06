using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class RegisterComapany
    {
        [Key]
        public int RegisterComapanyID { get; set; }
        [Display(Name ="Company Name")]
        public string Name { get; set; }
        [Display(Name ="Company Email")]
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name ="Comapny Logo")]
        public string logo { get; set; }
        //[Display(Name ="Licence No")]
        //public string Licence { get; set; }
        //public bool SendNotification { get; set; }
        //[Display(Name = "Licence Expiry Date")]
        //public DateTime LicenceExpiry { get; set; }
        //public string TRN { get; set; }
        [Display(Name = "Comapny Address")]
        public string Address { get; set; }
        [Display(Name ="Contact No")]
        public string Contact { get; set; }
        [Display(Name ="Enable Company")]
        public bool Enable { get; set; }
        //public string MacAddress { get; set; }
    }
}