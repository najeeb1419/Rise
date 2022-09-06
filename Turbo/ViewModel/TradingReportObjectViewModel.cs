using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risen.ViewModel.EmployeeAPIviewModel;

namespace Risen.ViewModel
{
    public class TradingReportObjectViewModel
    {
        public ResponseAPI response { get; set; }
        public List<TradingReportViewModel> tradingReportList { get; set; }
    }
}