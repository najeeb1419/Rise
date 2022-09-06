using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class EmployeePIPS
    {
        public int EmployeePIPSId { get; set; }
        public int PIPS { get; set; }
        public TradingSignals TradingSignals { get; set; }
        public int TradingSignalId { get; set; }
        public virtual CompanyEmployee CompanyEmployee { get; set; }
        public int  CompanyEmployeeID { get; set; }
        public string CompanyId { get; set; }
        public bool Disable { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Status { get; set; }
    }
}