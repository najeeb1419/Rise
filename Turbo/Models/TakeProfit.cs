using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class TakeProfit
    {
        public int TakeProfitId { get; set; }
        public int TradingSignalId { get; set; }
        public string TP { get; set; } //TP( take profit )
        public string PIPS { get; set; }


        public int? CreatedById { get; set; }
        public string Companyid { get; set; }
        public bool Disable { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public int No { get; set; }

        
    }
}