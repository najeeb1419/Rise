using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class TakeProfitViewModel
    {
        public int TakeProfitId { get; set; }
        public int TradingSignalId { get; set; }
        public string TP { get; set; } //TP( take profit )
        public string Points { get; set; }
        public bool Disble { get; set; }

        public TakeProfitViewModel()
        {
            TakeProfitId = 0;
            TradingSignalId = 0;
            TP = "";
            Points = "";
            Disble = false;
        }
    }
}