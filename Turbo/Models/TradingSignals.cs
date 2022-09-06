using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class TradingSignals
    {
        [Key]
        public int TradingSignalId { get; set; }
        //public int CurrenciesId { get; set; }
        //public virtual Currencies Currencies { get; set; }
        public virtual CurrencyList CurrencyList { get; set; }
        public int CurrencyListId { get; set; }
        public virtual CompanyEmployee CompanyEmployee { get; set; }
        public int CompanyEmployeeID { get; set; }
        public string Buy { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
        public string Image { get; set; }
        public string CurrentPrice { get; set; }
        public Int64 PIPS { get; set; }
        public string FromImage { get; set; }
        public string ToImage { get; set; }
        public string Status { get; set; }
        public int? EmployeeId { get; set; }

        public int? CreatedById { get; set; }
        public string Companyid { get; set; }
        public bool Disable { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}