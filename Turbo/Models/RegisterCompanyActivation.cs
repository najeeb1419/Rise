using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class RegisterCompanyActivation
    {
        [Key]
        public int RegisterCompanyActivationID { get; set; }
        public int RegisterComapanyid { get; set; }
        public Guid ActivationCode { get; set; }

    }
}