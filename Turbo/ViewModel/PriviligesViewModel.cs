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
        public string isClients { get; set; }
        public string isClientView { get; set; }
        public string isClientCreate { get; set; }
        public string isClientUpdate { get; set; }
        public string isClientDelet { get; set; }

        public string isLeadUser { get; set; }
        public string isLeadUserView { get; set; }
        public string isLeadUserUpdate { get; set; }
        public string isLeadUserConvertToCustomer { get; set; }

        public string isCompany { get; set; }
        public string isCompanyView { get; set; }
        public string isCompanyCreate { get; set; }
        public string isCompanyUpdate { get; set; }
        public string isCompanyDelet { get; set; }

        public string isDesignation { get; set; }
        public string isDesignationView { get; set; }
        public string isDesignationCreate { get; set; }
        public string isDesignationUpdate { get; set; }

        public string isProduct { get; set; }
        public string isProductView { get; set; }
        public string isProductUpdate { get; set; }
        public string isProductCreate { get; set; }

        public string isPolicy { get; set; }
        public string isPolicyView { get; set; }
        public string isPolicyCreate { get; set; }
        public string isPolicyUpdate { get; set; }

        public string isStaff { get; set; }
        public string isStaffView { get; set; }
        public string isStaffCreate { get; set; }
        public string isStaffUpdate { get; set; }
        public string isStaffDelet { get; set; }
        public string isLeadStaff { get; set; }
        public string isConverLeadPartner { get; set; }
        public string IsSetting { get; set; }

        public string IsDashboard { get; set; }
        public string MyCommission { get; set; }
        public string ViewPartnerCommisson { get; set; }
        public string IsReport { get; set; }

        public string isEmployee { get; set; }
        public string isEmployeeView { get; set; }
        public string isEmployeeCreate { get; set; }
        public string isEmployeeUpdate { get; set; }
    }
}