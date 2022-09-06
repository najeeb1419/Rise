using Risen.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risen.Contaxt;
using Risen.Models;

namespace Risen.Controllers
{
    public class DesignationController : Controller
    {
        RisenContext db = new RisenContext();
        // GET: Designation
        [HttpGet]
        public ActionResult DesignationView()
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                if (Session["Company"] != null)
                {
                    RegisterComapany comapany = new RegisterComapany();
                    comapany = Session["Company"] as RegisterComapany;
                    companyid = comapany.RegisterComapanyID;
                    name = comapany.Name;
                }
                else if (Session["Employee"] != null)
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                }
                var designation = db.Designations.Where(x => x.Enable == true && x.companyid== companyid.ToString()).ToList();
                return View(designation);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult DesignationView(string DesignationName)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                if (Session["Company"] != null)
                {
                    RegisterComapany comapany = new RegisterComapany();
                    comapany = Session["Company"] as RegisterComapany;
                    companyid = comapany.RegisterComapanyID;
                    name = comapany.Name;
                }
                else if (Session["Employee"] != null)
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                }
                var designations = db.Designations.Where(x => x.Enable == true && x.Name == DesignationName).ToList();
                if (designations.Count > 0)
                {
                    TempData["Designation"] = "Same name already exist.";
                    return RedirectToAction("DesignationView");
                }
                else
                {
                    Designation designation = new Designation();
                    designation.CreatedBy = name;
                    designation.CreatedTime = DateTime.Now.AddHours(5);
                    designation.ModifyTime = DateTime.Now.AddHours(5);
                    //designation. = StaffId;
                    designation.Enable = true;
                    designation.Name = DesignationName;
                    db.Designations.Add(designation);
                    db.SaveChanges();
                    return RedirectToAction("DesignationView");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public JsonResult FindDesignation(string Name)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                var Users = db.Designations.Where(x => x.Enable == true && x.Name == Name).ToList();
                if (User != null)
                {
                    return Json("exist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("expired", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult PreviligesView(int id)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {

                var privilige = db.privileges.Where(x=>x.DesignationId == id).FirstOrDefault();
                PriviligesViewModel priviligesVmodlObj = new PriviligesViewModel();
                if (privilige != null)
                {
                    if (privilige.isClients == true)
                    {
                        priviligesVmodlObj.isClients = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isClients = "";
                    }
                    if (privilige.isClientView == true)
                    {
                        priviligesVmodlObj.isClientView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isClientView = "";
                    }
                    if (privilige.isClientCreate == true)
                    {
                        priviligesVmodlObj.isClientCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isClientCreate = "";
                    }
                    if (privilige.isClientUpdate == true)
                    {
                        priviligesVmodlObj.isClientUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isClientUpdate = "";
                    }
                    if (privilige.isClientDelet == true)
                    {
                        priviligesVmodlObj.isClientDelet = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isClientDelet = "";
                    }
                    if (privilige.isLeadUser == true)
                    {
                        priviligesVmodlObj.isLeadUser = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isLeadUser = "";
                    }
                    if (privilige.isLeadUserView == true)
                    {
                        priviligesVmodlObj.isLeadUserView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isLeadUserView = "";
                    }
                    if (privilige.isLeadUserUpdate == true)
                    {
                        priviligesVmodlObj.isLeadUserUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isLeadUserUpdate = "";
                    }
                    if (privilige.isLeadUserConvertToCustomer == true)
                    {
                        priviligesVmodlObj.isLeadUserConvertToCustomer = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isLeadUserConvertToCustomer = "";
                    }
                    if (privilige.isCompany == true)
                    {
                        priviligesVmodlObj.isCompany = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isCompany = "";
                    }
                    if (privilige.isCompanyView == true)
                    {
                        priviligesVmodlObj.isCompanyView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isCompanyView = "";
                    }
                    if (privilige.isCompanyCreate == true)
                    {
                        priviligesVmodlObj.isCompanyCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isCompanyCreate = "";
                    }
                    if (privilige.isCompanyUpdate == true)
                    {
                        priviligesVmodlObj.isCompanyUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isCompanyUpdate = "";
                    }
                    if (privilige.isCompanyDelet == true)
                    {
                        priviligesVmodlObj.isCompanyDelet = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isCompanyDelet = "";
                    }
                    if (privilige.isDesignation == true)
                    {
                        priviligesVmodlObj.isDesignation = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isDesignation = "";
                    }
                    if (privilige.isDesignationCreate == true)
                    {
                        priviligesVmodlObj.isDesignationCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isDesignationCreate = "";
                    }
                    if (privilige.isDesignationView == true)
                    {
                        priviligesVmodlObj.isDesignationView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isDesignationView = "";
                    }
                    if (privilige.isDesignationUpdate == true)
                    {
                        priviligesVmodlObj.isDesignationUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isDesignationUpdate = "";
                    }
                    if (privilige.isProduct == true)
                    {
                        priviligesVmodlObj.isProduct = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isProduct = "";
                    }
                    if (privilige.isProductCreate == true)
                    {
                        priviligesVmodlObj.isProductCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isProductCreate = "";
                    }
                    if (privilige.isProductView == true)
                    {
                        priviligesVmodlObj.isProductView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isProductView = "";
                    }
                    if (privilige.isProductUpdate == true)
                    {
                        priviligesVmodlObj.isProductUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isProductUpdate = "";
                    }
                    if (privilige.isPolicy == true)
                    {
                        priviligesVmodlObj.isPolicy = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isPolicy = "";
                    }
                    if (privilige.isPolicyView == true)
                    {
                        priviligesVmodlObj.isPolicyView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isPolicyView = "";
                    }
                    if (privilige.isPolicyCreate == true)
                    {
                        priviligesVmodlObj.isPolicyCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isPolicyCreate = "";
                    }
                    if (privilige.isPolicyUpdate == true)
                    {
                        priviligesVmodlObj.isPolicyUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isPolicyUpdate = "";
                    }
                    if (privilige.isStaff == true)
                    {
                        priviligesVmodlObj.isStaff = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isStaff = "";
                    }
                    if (privilige.isStaffView == true)
                    {
                        priviligesVmodlObj.isStaffView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isStaffView = "";
                    }
                    if (privilige.isStaffCreate == true)
                    {
                        priviligesVmodlObj.isStaffCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isStaffCreate = "";
                    }
                    if (privilige.isStaffUpdate == true)
                    {
                        priviligesVmodlObj.isStaffUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isStaffUpdate = "";
                    }
                    if (privilige.isStaffDelet == true)
                    {
                        priviligesVmodlObj.isStaffDelet = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isStaffDelet = "";
                    }
                    if (privilige.IsSetting == true)
                    {
                        priviligesVmodlObj.IsSetting = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.IsSetting = "";
                    }
                    if (privilige.IsDashboard == true)
                    {
                        priviligesVmodlObj.IsDashboard = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.IsDashboard = "";
                    }
                    if (privilige.MyCommission)
                    {
                        priviligesVmodlObj.MyCommission = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.MyCommission = "";
                    }
                    if (privilige.ViewPartnerCommisson)
                    {
                        priviligesVmodlObj.ViewPartnerCommisson = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.ViewPartnerCommisson = "";
                    }
                    if (privilige.isLeadStaff)
                    {
                        priviligesVmodlObj.isLeadStaff = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isLeadStaff = "";
                    }
                    if (privilige.isConverLeadPartner)
                    {
                        priviligesVmodlObj.isConverLeadPartner = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isConverLeadPartner = "";
                    }

                    if (privilige.isEmployee)
                    {
                        priviligesVmodlObj.isEmployee = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isEmployee = "";
                    }
                    if (privilige.isEmployeeView)
                    {
                        priviligesVmodlObj.isEmployeeView = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isEmployeeView = "";
                    }
                    if (privilige.isEmployeeCreate)
                    {
                        priviligesVmodlObj.isEmployeeCreate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isEmployeeCreate = "";
                    }
                    if (privilige.isEmployeeUpdate)
                    {
                        priviligesVmodlObj.isEmployeeUpdate = "on";
                    }
                    else
                    {
                        priviligesVmodlObj.isEmployeeUpdate = "";
                    }
                    priviligesVmodlObj.PrivilegesId = privilige.PrivilegesId;
                }
                priviligesVmodlObj.DesignationId = id;

                return View(priviligesVmodlObj);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult PreviligesView(PriviligesViewModel privilegesVM)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                if (Session["Company"] != null)
                {
                    RegisterComapany comapany = new RegisterComapany();
                    comapany = Session["Company"] as RegisterComapany;
                    companyid = comapany.RegisterComapanyID;
                    name = comapany.Name;
                }
                else if (Session["Employee"] != null)
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.CompanyEmployeeID;
                    name = employee1.fName + " " + employee1.lName;
                }

                Privileges privilige = new Privileges();
                if (privilegesVM.PrivilegesId > 0)
                {
                    privilige = db.privileges.Where(x =>x.PrivilegesId == privilegesVM.PrivilegesId).FirstOrDefault();
                }
                PriviligesViewModel priviligesVmodlObj = new PriviligesViewModel();

                if (privilegesVM.isClients == "on")
                {
                    privilige.isClients = true;
                }
                else
                {
                    privilige.isClients = false;
                }
                if (privilegesVM.isClientView == "on")
                {
                    privilige.isClientView = true;
                }
                else
                {
                    privilige.isClientView = false;
                }
                if (privilegesVM.isClientCreate == "on")
                {
                    privilige.isClientCreate = true;
                }
                else
                {
                    privilige.isClientCreate = false;
                }
                if (privilegesVM.isClientUpdate == "on")
                {
                    privilige.isClientUpdate = true;
                }
                else
                {
                    privilige.isClientUpdate = false;
                }
                if (privilegesVM.isClientDelet == "on")
                {
                    privilige.isClientDelet = true;
                }
                else
                {
                    privilige.isClientDelet = false;
                }
                if (privilegesVM.isLeadUser == "on")
                {
                    privilige.isLeadUser = true;
                }
                else
                {
                    privilige.isLeadUser = false;
                }
                if (privilegesVM.isLeadUserView == "on")
                {
                    privilige.isLeadUserView = true;
                }
                else
                {
                    privilige.isLeadUserView = false;
                }
                if (privilegesVM.isLeadUserUpdate == "on")
                {
                    privilige.isLeadUserUpdate = true;
                }
                else
                {
                    privilige.isLeadUserUpdate = false;
                }
                if (privilegesVM.isLeadUserConvertToCustomer == "on")
                {
                    privilige.isLeadUserConvertToCustomer = true;
                }
                else
                {
                    privilige.isLeadUserConvertToCustomer = false;
                }
                if (privilegesVM.isCompany == "on")
                {
                    privilige.isCompany = true;
                }
                else
                {
                    privilige.isCompany = false;
                }
                if (privilegesVM.isCompanyView == "on")
                {
                    privilige.isCompanyView = true;
                }
                else
                {
                    privilige.isCompanyView = false;
                }
                if (privilegesVM.isCompanyCreate == "on")
                {
                    privilige.isCompanyCreate = true;
                }
                else
                {
                    privilige.isCompanyCreate = false;
                }
                if (privilegesVM.isCompanyUpdate == "on")
                {
                    privilige.isCompanyUpdate = true;
                }
                else
                {
                    privilige.isCompanyUpdate = false;
                }
                if (privilegesVM.isCompanyDelet == "on")
                {
                    privilige.isCompanyDelet = true;
                }
                else
                {
                    privilige.isCompanyDelet = false;
                }
                if (privilegesVM.isDesignation == "on")
                {
                    privilige.isDesignation = true;
                }
                else
                {
                    privilige.isDesignation = false;
                }
                if (privilegesVM.isDesignationView == "on")
                {
                    privilige.isDesignationView = true;
                }
                else
                {
                    privilige.isDesignationView = false;
                }
                if (privilegesVM.isDesignationCreate == "on")
                {
                    privilige.isDesignationCreate = true;
                }
                else
                {
                    privilige.isDesignationCreate = false;
                }
                if (privilegesVM.isDesignationUpdate == "on")
                {
                    privilige.isDesignationUpdate = true;
                }
                else
                {
                    privilige.isDesignationUpdate = false;
                }
                if (privilegesVM.isProduct == "on")
                {
                    privilige.isProduct = true;
                }
                else
                {
                    privilige.isProduct = false;
                }
                if (privilegesVM.isProductView == "on")
                {
                    privilige.isProductView = true;
                }
                else
                {
                    privilige.isProductView = false;
                }
                if (privilegesVM.isProductUpdate == "on")
                {
                    privilige.isProductUpdate = true;
                }
                else
                {
                    privilige.isProductUpdate = false;
                }
                if (privilegesVM.isProductCreate == "on")
                {
                    privilige.isProductCreate = true;
                }
                else
                {
                    privilige.isProductCreate = false;
                }
                if (privilegesVM.isPolicy == "on")
                {
                    privilige.isPolicy = true;
                }
                else
                {
                    privilige.isPolicy = false;
                }
                if (privilegesVM.isPolicyView == "on")
                {
                    privilige.isPolicyView = true;
                }
                else
                {
                    privilige.isPolicyView = false;
                }
                if (privilegesVM.isPolicyCreate == "on")
                {
                    privilige.isPolicyCreate = true;
                }
                else
                {
                    privilige.isPolicyCreate = false;
                }
                if (privilegesVM.isPolicyUpdate == "on")
                {
                    privilige.isPolicyUpdate = true;
                }
                else
                {
                    privilige.isPolicyUpdate = false;
                }
                if (privilegesVM.isStaff == "on")
                {
                    privilige.isStaff = true;
                }
                else
                {
                    privilige.isStaff = false;
                }
                if (privilegesVM.isStaffView == "on")
                {
                    privilige.isStaffView = true;
                }
                else
                {
                    privilige.isStaffView = false;
                }
                if (privilegesVM.isStaffCreate == "on")
                {
                    privilige.isStaffCreate = true;
                }
                else
                {
                    privilige.isStaffCreate = false;
                }
                if (privilegesVM.isStaffUpdate == "on")
                {
                    privilige.isStaffUpdate = true;
                }
                else
                {
                    privilige.isStaffUpdate = false;
                }
                if (privilegesVM.isStaffDelet == "on")
                {
                    privilige.isStaffDelet = true;
                }
                else
                {
                    privilige.isStaffDelet = false;
                }
                if (privilegesVM.IsSetting == "on")
                {
                    privilige.IsSetting = true;
                }
                else
                {
                    privilige.IsSetting = false;
                }
                if (privilegesVM.IsDashboard == "on")
                {
                    privilige.IsDashboard = true;
                }
                else
                {
                    privilige.IsDashboard = false;
                }
                if (privilegesVM.MyCommission == "on")
                {
                    privilige.MyCommission = true;
                }
                else
                {
                    privilige.MyCommission = false;
                }
                if (privilegesVM.ViewPartnerCommisson == "on")
                {
                    privilige.ViewPartnerCommisson = true;
                }
                else
                {
                    privilige.ViewPartnerCommisson = false;
                }
                if (privilegesVM.isLeadStaff == "on")
                {
                    privilige.isLeadStaff = true;
                }
                else
                {
                    privilige.isLeadStaff = false;
                }
                if (privilegesVM.isConverLeadPartner == "on")
                {
                    privilige.isConverLeadPartner = true;
                }
                else
                {
                    privilige.isConverLeadPartner = false;
                }

                if (privilegesVM.isEmployee == "on")
                {
                    privilige.isEmployee = true;
                }
                else
                {
                    privilige.isEmployee = false;
                }
                if (privilegesVM.isEmployeeView == "on")
                {
                    privilige.isEmployeeView = true;
                }
                else
                {
                    privilige.isEmployeeView = false;
                }
                if (privilegesVM.isEmployeeCreate == "on")
                {
                    privilige.isEmployeeCreate = true;
                }
                else
                {
                    privilige.isEmployeeCreate = false;
                }
                if (privilegesVM.isEmployeeUpdate == "on")
                {
                    privilige.isEmployeeUpdate = true;
                }
                else
                {
                    privilige.isEmployeeUpdate = false;
                }
                privilige.PrivilegesId = privilegesVM.PrivilegesId;
                if (privilegesVM.PrivilegesId > 0)
                {
                    privilige.UpdatedTime = DateTime.Now.AddHours(5);
                    privilige.MoyfiedBy = name;        
                    //privilige.StaffId = StaffId;
                    db.Entry(privilige).State = EntityState.Modified;
                    privilige.CompanyId = companyid.ToString();
                    privilige.Enalbe = true;
                    db.SaveChanges();
                    TempData["message"] = "update";
                    TempData.Keep();
                }
                else
                {
                    privilige.CreatedBy = name;
                    privilige.CreatedTime = DateTime.Now.AddHours(5);
                    privilige.UpdatedTime = DateTime.Now.AddHours(5);
                    privilige.DesignationId = privilegesVM.DesignationId;
                    privilige.CompanyId = companyid.ToString();
                    privilige.Enalbe = true;
                    db.privileges.Add(privilige);
                    db.SaveChanges();
                    TempData["message"] = "update";
                    TempData.Keep();
                }


                return RedirectToAction("DesignationView");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
    }
}