using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class CompanyEmployeesApi
    {
        public int CompanyEmployeeID { get; set; }
        [Display(Name = "First Name")]
        public string fName { get; set; }
        [Display(Name = "Last Name")]
        public string lName { get; set; }
        [Display(Name = "Employee Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Employee Image")]
        public string Image { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
        public string Companyid { get; set; }
        [Display(Name = "Employee Address")]
        public string Address { get; set; }
        [Display(Name = "Contact No")]
        public string Contact { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Enable Employee")]
        public bool Enable { get; set; }
        public string code { get; set; }
        public string Message { get; set; }
        public string mobile_model { get; set; }
        public string os_version { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string companyid { get; set; }
        public string employeeid { get; set; }
    }
}