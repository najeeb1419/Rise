using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class ConfirmationCode
    {
        [Key]
        public int ConfirmationCodeID { get; set; }
        public string employeeid { get; set; }
        public string ParentId { get; set; }
        public string CustomerId { get; set; }
        public string companyid { get; set; }
        public string code { get; set; }
        public string uniquekey { get; set; }

    }
}