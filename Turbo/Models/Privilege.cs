using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.Models
{
    public class Privileges
    {
        public int PrivilegesId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public virtual Designation Designation { get; set; }
        public int DesignationId { get; set; }
        public string CompanyId { get; set; }

        public bool Enalbe { get; set; }

        public bool isCurrency { get; set; }
        public bool isCurrencyView { get; set; }
        public bool isCurrencyCreate { get; set; }
        public bool isCurrencyUpdate { get; set; }
        public bool isCurrencyDelete { get; set; }

        public bool isDesignation { get; set; }
        public bool isDesignationView { get; set; }
        public bool isDesignationUpdate { get; set; }
        public bool isDesignationCreate { get; set; }

        public bool isTradeIdea { get; set; }
        public bool isTradeIdeaView { get; set; }
        public bool isTradeIdeaCreate { get; set; }
        public bool isTradeIdeaUpdate { get; set; }
        public bool isTradeIdeaDelete { get; set; }
        public bool isAddTakeProfit { get; set; }
        public bool isAddStopLoss { get; set; }

        public bool isDashboard { get; set; }
        public bool isSetting { get; set; }

        public bool isEmployee { get; set; }
        public bool isEmployeeView { get; set; }
        public bool isEmployeeCreate { get; set; }
        public bool isEmployeeUpdate { get; set; }
        public bool isManager { get; set; }

    }
}