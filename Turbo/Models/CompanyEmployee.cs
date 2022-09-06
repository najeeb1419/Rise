using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class CompanyEmployee
    {
        [Key]
        public int CompanyEmployeeID { get; set; }
        [Display(Name ="First Name")]
        public string fName { get; set; }
        [Display(Name ="Last Name")]
        public string lName { get; set; }
        [Display(Name = "Employee Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Employee Image")]
        public string Image { get; set; }
        [Display(Name ="Total Salary")]
        public string Salary { get; set; }
   
        [Display(Name ="Joining Date")]
        public DateTime JoiningDate { get; set; }
      
        public int Companyid { get; set; }
        [Display(Name = "Employee Address")]
        public string Address { get; set; }
        [Display(Name = "Contact No")]
        public string Contact { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModyfiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModyfiedBy { get; set; }
        [Display(Name = "Enable Employee")]
        public bool Enable { get; set; }
        public string DateOfBirth { get; set; }
        public virtual Designation Designation { get; set; }
        public int DesignationId { get; set; }
        public int? CreatedById { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsHide { get; set; }


        public string UserName { get; set; }

       
    }
}