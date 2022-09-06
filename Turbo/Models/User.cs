using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int DeviceUserId { get; set; }
        public string ApiToken { get; set; }
        public string DeviceToken { get; set; }
        public bool Disable { get; set; }

        //public virtual CompanyEmployee CompanyEmployee { get; set; }
        //public int CompanyEmployeeID { get; set; }
        public virtual RegisterComapany RegisterComapany { get; set; }
        public int RegisterComapanyID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}