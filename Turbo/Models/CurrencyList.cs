using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class CurrencyList
    {
        public int CurrencyListId { get; set; }
        public string CurrencyName { get; set; }
        public int CurrencyNo { get; set; }
        public string firstImage { get; set; }
        public string SecondImage { get; set; }
    }
}