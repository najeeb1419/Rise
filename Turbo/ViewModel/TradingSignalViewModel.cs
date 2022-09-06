using AutoMapper;
using Risen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    [AutoMap(typeof(TradingSignals))]

    public class TradingSignalViewModel
    {
        public int TradingSignalId { get; set; }
        public int CurrencyListId { get; set; }
        public int CurrencyNo { get; set; }
        public string CurrencyName { get; set; }
        public string Image { get; set; }
        public string Buy { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
        public string CreatedByImageImage { get; set; }
        public string CurrentPrice { get; set; }
        public Int64 Points { get; set; }
        public string CurrencyImage { get; set; }
        public string Companyid { get; set; }
        public string CreatedBy { get; set; }
        public int? CreatedId { get; set; }
        public string CreatedTime { get; set; } 
        public string LatestHitTp { get; set; }
        public string Status { get; set; }
        public string CurrencyDisplayName { get; set; }
        //public string ModifyBy { get; set; }
        //public DateTime ModifyTime { get; set; }
        public List<TakeProfitViewModel> TakeProfitList { get; set; }
        public StopLoseViewModel StopLose { get; set; }
        public TradingSignalViewModel()
        {
            CurrencyListId = 0;
            TradingSignalId = 0;
            CurrencyNo = 0;
            Buy = "";
            Type = "";
            Remarks = "";
            CurrentPrice = "";
            CreatedByImageImage = "";
            CurrentPrice = "";
            Points = 0;
            CurrencyName = "";
            CurrencyImage = "";
            Companyid = "";
            CreatedBy = "";
            CreatedTime = "";
            LatestHitTp = "";
            Status = "";
        }
    }
}