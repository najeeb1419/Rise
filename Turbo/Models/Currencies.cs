using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class Currencies
    {
        public int CurrenciesId { get; set; }
        public virtual CurrencyList CurrencyList { get; set; }
        public int CurrencyListId { get; set; }
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public string FirstCurrencyImage { get; set; }
        public string SecondCurrencyImage { get; set; }
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