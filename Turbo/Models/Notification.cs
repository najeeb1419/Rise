using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public virtual RegisterComapany RegisterComapany { get; set; }
        public virtual CompanyEmployee CompanyEmployee { get; set; }
        public int CompanyEmployeeID { get; set; }
        public int RegisterComapanyID { get; set; }
        public int CreatedById { get; set; }
        public string CurrencyName { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string   Price { get; set; }
        public DateTime CreatedTime { get; set; }
        public string TradingSignalId { get; set; }
    }
}