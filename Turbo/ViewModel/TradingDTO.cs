using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risen.ViewModel.EmployeeAPIviewModel;

namespace Risen.ViewModel
{
    public class TradingDTO
    {
        public ResponseAPI Response { get; set; }
        public List<TradingSignalViewModel> TradingSignalList { get; set; }
    
    }
}