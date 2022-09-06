using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class ApiDTO
    {
        public int companyId { get; set; }
        public int employeeId { get; set; }
        public string reportType { get; set; }
        public string apiToken { get; set; }
        public int tradingSignalId { get; set; }
        public string date { get; set; }

        public ApiDTO()
        {
            companyId = 0;
            employeeId = 0;
            reportType = "";
            apiToken = "";
            tradingSignalId = 0;
            date = "";
        }
    }
}