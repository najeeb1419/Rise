using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class PastResultViewModel
    {
        public int TradingSignalId { get; set; }
        public string CurrencyName { get; set; }
        public string Type { get; set; }
        public string CreatedTime { get; set; }
        public string Badges { get; set; }
        public string CurrencyDisplayName { get; set; }
    }
}