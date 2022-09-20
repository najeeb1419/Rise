using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risen.Contaxt;
using Risen.Models;

namespace Risen.Controllers
{
    public class AuthenticationController : Controller
    {
        RisenContext db = new RisenContext();
        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                CompanyEmployee employee = new CompanyEmployee();
                RegisterComapany company = new RegisterComapany();
                employee = db.CompanyEmployees.Where(x => x.Enable == true && x.Email == Email && x.Password == Password).FirstOrDefault();
                company = db.RegisterComapany.Where(x => x.Enable == true && x.Email == Email && x.Password == Password).FirstOrDefault();
                if (employee != null)
                {
                    Session["Employee"] = employee;
                    var Privilige = db.privileges.Where(x => x.Enalbe == true && x.DesignationId == employee.DesignationId).FirstOrDefault();
                    if (Privilige == null)
                    {
                        TempData["mdg"] = "Sorry You don't have any access please contact to the admin.";
                        TempData.Keep();
                        return RedirectToAction("Login");
                    }
                    Session["Priviliges"] = Privilige;

                    return RedirectToAction("Index", "Home");

                }
                else if (company != null)
                {
                    Session["Company"] = company;
                    CompanyPrivilege();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["mdg"] = "Wrong email or password!";
                    TempData.Keep();
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["mdg"] = ex.Message;
                TempData.Keep();
                return View();
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        public void CompanyPrivilege()
        {
            Privileges privileges = new Privileges();
            privileges.isCurrency = true;
            privileges.isCurrencyView = true;
            privileges.isCurrencyCreate = true;
            privileges.isCurrencyUpdate = true;
            privileges.isCurrencyDelete = true;

            privileges.isDesignation = true;
            privileges.isDesignationView = true;
            privileges.isDesignationUpdate = true;
            privileges.isDesignationCreate = true;

            privileges.isTradeIdea = true;
            privileges.isTradeIdeaView = true;
            privileges.isTradeIdeaCreate = true;
            privileges.isTradeIdeaUpdate = true;
            privileges.isTradeIdeaDelete = true;
            privileges.isAddTakeProfit = true;
            privileges.isAddStopLoss = true;

            privileges.isEmployee = true;
            privileges.isEmployeeView = true;
            privileges.isEmployeeCreate = true;
            privileges.isEmployeeUpdate = true;

            privileges.isDashboard = true;
            privileges.isSetting = true;
            privileges.isManager = true;
            Session["Priviliges"] = privileges;
        }
    }
}