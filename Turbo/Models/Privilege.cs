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
        public string MoyfiedBy { get; set; }
        public virtual Designation Designation { get; set; }
        public int DesignationId { get; set; }
        public string CompanyId { get; set; }

        public bool Enalbe { get; set; }

        public bool isClients { get; set; }
        public bool isClientView { get; set; }
        public bool isClientCreate { get; set; }
        public bool isClientUpdate { get; set; }
        public bool isClientDelet { get; set; }

        public bool isLeadUser { get; set; }
        public bool isLeadUserView { get; set; }
        public bool isLeadUserUpdate { get; set; }
        public bool isLeadUserConvertToCustomer { get; set; }

        public bool isCompany { get; set; }
        public bool isCompanyView { get; set; }
        public bool isCompanyCreate { get; set; }
        public bool isCompanyUpdate { get; set; }
        public bool isCompanyDelet { get; set; }

        public bool isDesignation { get; set; }
        public bool isDesignationView { get; set; }
        public bool isDesignationUpdate { get; set; }
        public bool isDesignationCreate { get; set; }

        public bool isProduct { get; set; }
        public bool isProductCreate { get; set; }
        public bool isProductView { get; set; }
        public bool isProductUpdate { get; set; }

        public bool isPolicy { get; set; }
        public bool isPolicyView { get; set; }
        public bool isPolicyCreate { get; set; }
        public bool isPolicyUpdate { get; set; }

        public bool isStaff { get; set; }
        public bool isStaffView { get; set; }
        public bool isStaffCreate { get; set; }
        public bool isStaffUpdate { get; set; }
        public bool isStaffDelet { get; set; }
        public bool isLeadStaff { get; set; }
        public bool isConverLeadPartner { get; set; }

        public bool IsDashboard { get; set; }
        public bool MyCommission { get; set; }
        public bool ViewPartnerCommisson { get; set; }
        public bool IsReport { get; set; }
        public bool IsSetting { get; set; }

        public bool isEmployee         { get; set; }
        public bool isEmployeeView      { get; set; }
        public bool isEmployeeCreate    { get; set; }
        public bool isEmployeeUpdate   { get; set; }
    }
}