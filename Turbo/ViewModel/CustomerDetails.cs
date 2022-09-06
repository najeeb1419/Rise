using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class CustomerDetails
    {
   
        public string Package { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Visits { get; set; }
        public string Price { get; set; }
        public string Paid { get; set; }
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Discount { get; set; }
    }
}