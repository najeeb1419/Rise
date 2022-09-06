using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class NotificationViewModel
    {
        public string title { get; set; }
        public string body { get; set; }
        public string createdTime { get; set; }
        public int companyId { get; set; }
        public string employeeName { get; set; }
        public string status { get; set; }
        public string currencyName { get; set; }
        public string type { get; set; }
        public string price { get; set; }
        public string TradingSignalId { get; set; }
        public int CreatedById { get; set; }

        public NotificationViewModel()
        {
            title = "";
            body = "";
            createdTime = "";
            companyId = 0;
            employeeName = "";
            status = "";
            currencyName = "";
            type = "";
            price = "";
            TradingSignalId = "";
            CreatedById = 0;

        }
    }
}