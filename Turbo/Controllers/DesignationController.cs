using Risen.Contaxt;
using Risen.Models;
using Risen.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

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
                var designation = db.Designations.Where(x => x.Enable == true && x.companyid == companyid.ToString()).ToList();
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
                var designations = db.Designations.Where(x => x.Enable == true && x.Name == DesignationName && x.companyid==companyid.ToString()).ToList();
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
                    designation.companyid = companyid.ToString();
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

                var privilige = db.privileges.Where(x => x.DesignationId == id).FirstOrDefault();
                PriviligesViewModel priviligesVmodlObj = new PriviligesViewModel();
                if (privilige != null)
                {

                    foreach (PropertyInfo propertyInfo in privilige.GetType().GetProperties())
                    {
                        if (propertyInfo.PropertyType == typeof(bool) && Convert.ToBoolean(propertyInfo.GetValue(privilige, null)))
                        {
                            foreach (PropertyInfo info in priviligesVmodlObj.GetType().GetProperties())
                            {
                                if (info.Name == propertyInfo.Name)
                                {
                                    info.SetValue(priviligesVmodlObj, "on");
                                }
                            }
                        }
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
                    privilige = db.privileges.Where(x => x.PrivilegesId == privilegesVM.PrivilegesId).FirstOrDefault();
                }
                if (privilegesVM != null)
                {
                    foreach (PropertyInfo propertyInfo in privilegesVM.GetType().GetProperties())
                    {
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            foreach (PropertyInfo info in privilige.GetType().GetProperties())
                            {
                                if (info.Name == propertyInfo.Name)
                                {
                                    var val = (dynamic)null;
                                    if (propertyInfo.GetValue(privilegesVM, null) != null)
                                    {
                                        val = propertyInfo.GetValue(privilegesVM, null).ToString() == "on" ? true : false;
                                    }
                                    info.SetValue(privilige, val);
                                }
                            }
                        }
                    }
                }
                privilige.UpdatedTime = DateTime.Now.AddHours(5);
                if (privilegesVM.PrivilegesId > 0)
                {
                    privilige.ModifiedBy = name;
                    db.Entry(privilige).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "update";
                    TempData.Keep();
                }
                else
                {
                    privilige.CreatedBy = name;
                    privilige.CreatedTime = DateTime.Now.AddHours(5);
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