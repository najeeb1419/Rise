using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class AllowNotification
    {
        public int AllowNotificationId { get; set; }
        public int DeviceUserId { get; set; }
        public virtual CompanyEmployee CompanyEmployee { get; set; }
        public int CompanyEmployeeID { get; set; }
        public RegisterComapany RegisterComapany { get; set; }
        public int RegisterComapanyID { get; set; }
        public bool status { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}