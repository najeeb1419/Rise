using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string deviceToken { get; set; }
        public string apiToken { get; set; }
        public int companyid { get; set; }

        public UserDTO()
        {
            userId = 0;
            deviceToken = "";
            apiToken = "";
            companyid = 0;
        }
    }
}