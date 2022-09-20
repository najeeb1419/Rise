using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class PriviligesViewModel
    {
        public int PrivilegesId { get; set; }
        public int DesignationId { get; set; }
        public string isCurrency { get; set; }
        public string isCurrencyView { get; set; }
        public string isCurrencyCreate { get; set; }
        public string isCurrencyUpdate { get; set; }
        public string isCurrencyDelet { get; set; }

        public string isDesignation { get; set; }
        public string isDesignationView { get; set; }
        public string isDesignationUpdate { get; set; }
        public string isDesignationCreate { get; set; }

        public string isTradeIdea { get; set; }
        public string isTradeIdeaView { get; set; }
        public string isTradeIdeaCreate { get; set; }
        public string isTradeIdeaUpdate { get; set; }
        public string isTradeIdeaDelet { get; set; }
        public string isAddTakeProfit { get; set; }
        public string isAddStopLoss { get; set; }

        public string isDashboard { get; set; }
        public string isSetting { get; set; }

        public string isEmployee { get; set; }
        public string isEmployeeView { get; set; }
        public string isEmployeeCreate { get; set; }
        public string isEmployeeUpdate { get; set; }
        public string isManager { get; set; }

    }
}