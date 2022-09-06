using Risen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risen.Contaxt;

namespace Risen.Controllers
{
    public class EmployeeController : Controller
    {
        RisenContext db = new RisenContext();

        // GET: Employee
        [HttpGet]
        public ActionResult EmployeeView()
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                CompanyEmployee employee1 = new CompanyEmployee();
                employee1 = Session["Employee"] as CompanyEmployee;
                if (employee1.Designation.Name.ToLower() == "admin")
                {
                    ViewBag.admin = "admin";
                }
                 else
                {
                    ViewBag.admin = "";
                }
                var Employeelist = db.CompanyEmployees.Where(x => x.Enable == true).ToList();
                var Designation = db.Designations.Where(x => x.Enable == true).ToList();
                ViewBag.Designation = Designation;

                return View(Employeelist);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult EmployeeView(CompanyEmployee employee)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                   
                }
                
                employee.CreatedDate = DateTime.Now.AddHours(5);
                employee.ModyfiedDate = DateTime.Now.AddHours(5);
                employee.Enable = true;
                employee.CreatedBy = name;
                employee.Companyid = companyid;
                employee.JoiningDate = DateTime.Now.AddHours(5);
                employee.EmailConfirmed = true;
                TempData["msg"] = null;
                TempData.Keep();
                if (CheckExist(employee.Contact, employee.Email))
                {
                    TempData["msg"] = "exist";
                    TempData.Keep();
                    return RedirectToAction("EmployeeView");
                }
                db.CompanyEmployees.Add(employee);
                db.SaveChanges();
                TempData["msg"] = "success";
                TempData.Keep();
                return RedirectToAction("EmployeeView");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        
        public ActionResult EditEmployee(int id)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                var EmployeeObj = db.CompanyEmployees.Where(x => x.Enable == true && x.CompanyEmployeeID == id).FirstOrDefault();
                var Designation = db.Designations.Where(x => x.Enable == true).ToList();
                DateTime Dob = Convert.ToDateTime(EmployeeObj.DateOfBirth);
                EmployeeObj.DateOfBirth = Dob.ToString("dd-MM-yyyy");
                ViewBag.Designation = Designation;
                return View(EmployeeObj);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public ActionResult EditEmployee(CompanyEmployee employee)
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
                var findemployee = db.CompanyEmployees.Where(x => x.Enable == true && x.CompanyEmployeeID == employee.CompanyEmployeeID).FirstOrDefault();
                if (findemployee != null)
                {
                    findemployee.fName = employee.fName;
                    findemployee.lName = employee.lName;
                    findemployee.Email = employee.Email;
                    findemployee.Contact = employee.Contact;
                    findemployee.DesignationId = employee.DesignationId;
                    findemployee.Address = employee.Address;
                    findemployee.DateOfBirth = employee.DateOfBirth;
                    findemployee.Password = employee.Password;
                    if (employee.Image != "" && employee.Image != null)
                    {
                        findemployee.Image = employee.Image;
                    }
                    findemployee.ModyfiedBy = name;
                    findemployee.ModyfiedDate = DateTime.Now.AddHours(5);
                    db.Entry(findemployee).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "eidt";
                    TempData.Keep();
                    return RedirectToAction("EmployeeView");

                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

        }

        public bool CheckExist(string contact, string mail)
        {
            bool result = false;
            var findemployeeContact = db.CompanyEmployees.Where(x => x.Contact == contact && x.Enable == true).Count();
            var findemployeemail = db.CompanyEmployees.Where(x => x.Email == mail && x.Enable == true).Count();
            var CompanyContact = db.RegisterComapany.Where(x => x.Email == mail && x.Enable == true).Count();
            var findstaffmail = db.RegisterComapany.Where(x => x.Contact == contact && x.Enable == true).Count();
            if (findemployeeContact > 0 || findemployeemail > 0 || CompanyContact > 0 || findstaffmail > 0)
            {
                result = true;
            }
            return result;
        }

        [HttpGet]
        public JsonResult CheckEmployee(string contact, int id)
        {
            string msg = "";
            if (id > 0)
            {
                var findemployee = db.CompanyEmployees.Where(x => x.Contact == contact && x.Enable == true && x.CompanyEmployeeID != id).FirstOrDefault();
                var findcomapany = db.RegisterComapany.Where(x => x.Contact == contact && x.Enable == true).FirstOrDefault();
                if (findemployee == null && findcomapany == null)
                {
                    msg = "ok";
                }
                else
                {
                    msg = "no";
                }

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var findemployee = db.CompanyEmployees.Where(x => x.Contact == contact && x.Enable == true).FirstOrDefault();
                var findstaff = db.RegisterComapany.Where(x => x.Contact == contact && x.Enable == true).FirstOrDefault();
                if (findemployee == null && findstaff == null)
                {
                    msg = "ok";
                }
                else
                {
                    msg = "no";
                }

                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public JsonResult CheckEmployeeMail(string mail, int id)
        {
            if (id > 0)
            {
                string msg = "";
                var findemployee = db.CompanyEmployees.Where(x => x.Email == mail && x.Enable == true && x.CompanyEmployeeID == id).FirstOrDefault();
                var findcompany = db.RegisterComapany.Where(x => x.Email == mail && x.Enable == true).FirstOrDefault();
                if (findemployee == null && findcompany == null)
                {
                    msg = "ok";
                }
                else
                {
                    msg = "no";
                }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string msg = "";
                var findemployee = db.CompanyEmployees.Where(x => x.Email == mail && x.Enable == true).FirstOrDefault();
                var findCompany = db.RegisterComapany.Where(x => x.Email == mail && x.Enable == true).FirstOrDefault();
                if (findemployee == null && findCompany == null)
                {
                    msg = "ok";
                }
                else
                {
                    msg = "no";
                }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult EmployeeImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string subPath = "~/Images/Employee/";
                bool isExist = Directory.Exists(Server.MapPath(subPath));
                if (isExist == false)
                {
                    Directory.CreateDirectory(Server.MapPath(subPath));
                }
                else
                {
                    file.SaveAs(HttpContext.Server.MapPath(subPath) + fileName);
                    result.Data = new { Success = true, ImageURL = string.Format(fileName) };
                }
            }
            catch (Exception ex)
            {
                result.Data = ex.Message;
            }
            return result;
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                CompanyEmployee employee = new CompanyEmployee();
                employee = Session["Employee"] as CompanyEmployee;
                if (employee.Designation.Name.ToLower() == "admin")
                {
                    ViewBag.admin = "admin";
                }
                else
                {
                    ViewBag.admin = "";
                }
                employee = Session["Employee"] as CompanyEmployee;
                var Employee = db.CompanyEmployees.Where(x => x.Enable == true && x.CompanyEmployeeID == employee.CompanyEmployeeID).FirstOrDefault();
                var Designation = db.Designations.Where(x => x.Enable == true).ToList();
                ViewBag.Designation = Designation;
                ViewBag.LoginUser = Employee;
                Employee.Designation = Employee.Designation;
                Employee.DesignationId = Employee.DesignationId;
                return View(Employee);
                //}
            }

            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public ActionResult EditProfile()
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
                CompanyEmployee employee = new CompanyEmployee();
                employee = Session["employee"] as CompanyEmployee;
                var Employee = db.CompanyEmployees.Where(x => x.Enable == true && x.CompanyEmployeeID == employee.CompanyEmployeeID).FirstOrDefault();
                var Designation = db.Designations.Where(x => x.Enable == true).ToList();
                ViewBag.Designation = Designation;
                ViewBag.LoginUser = Employee;
                employee.Designation = Employee.Designation;
                employee.DesignationId = Employee.DesignationId;
                return View(Employee);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(CompanyEmployee userProfile)
        {
            if (Session["staff"] != null || Session["employee"] != null)
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
                CompanyEmployee employee = new CompanyEmployee();
                employee = Session["employee"] as CompanyEmployee;
                var Employee = db.CompanyEmployees.Where(x => x.Enable == true && x.CompanyEmployeeID == employee.CompanyEmployeeID).FirstOrDefault();

                Employee.fName = userProfile.fName;
                Employee.lName = userProfile.lName;
                Employee.DateOfBirth = userProfile.DateOfBirth;
                Employee.Address = userProfile.Address;
                if (userProfile.Image != "" && userProfile.Image != null)
                {
                    Employee.Image = userProfile.Image;
                }
                Employee.ModyfiedDate = DateTime.Now.AddHours(5);
                Employee.ModyfiedBy = employee.fName + " " + employee.lName;
                Employee.CreatedById = companyid;
                db.Entry(Employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
    }
}