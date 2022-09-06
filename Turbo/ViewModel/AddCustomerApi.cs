using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class AddCustomerApi
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contactno { get; set; }
        public string Carno { get; set; }
        public string employeeid { get; set; }
        public string companyid { get; set; }
        public DateTime CreatedTime { get; set; }
        public string FirebaseKey { get; set; }
    }
}