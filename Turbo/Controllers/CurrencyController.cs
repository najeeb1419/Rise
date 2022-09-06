using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risen.Contaxt;
using Risen.Models;

namespace Risen.Controllers
{
    public class CurrencyController : Controller
    {
        RisenContext db = new RisenContext();
        List<CurrencyList> currency = new List<CurrencyList>();
        // GET: Currency
        [HttpGet]

                
        public void SaveCurrencies()
        {
          
            currency.Add(new CurrencyList() { CurrencyName = "GDAXI", CurrencyNo = 3 });
            currency.Add(new CurrencyList() { CurrencyName = "DJI", CurrencyNo = 1 });
            currency.Add(new CurrencyList() { CurrencyName = "NDX", CurrencyNo = 5 });
            currency.Add(new CurrencyList() { CurrencyName = "GSPC", CurrencyNo = 4 });
            currency.Add(new CurrencyList() { CurrencyName = "FTSE", CurrencyNo = 12 });
            currency.Add(new CurrencyList() { CurrencyName = "FCHI", CurrencyNo = 2 });
            currency.Add(new CurrencyList() { CurrencyName = "NSEI", CurrencyNo = 6 });
            //currency.Add(new CurrencyList() { CurrencyName = "CHINA", CurrencyNo = 35 });
            //currency.Add(new CurrencyList() { CurrencyName = "INDIA", CurrencyNo = 50 });
        }


        public ActionResult CurrencyView()
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                //SaveCurrencies();
                //foreach (var item in currency)
                //{
                //    db.CurrencyLists.Add(item);
                //    db.SaveChanges();
                //}

                ViewBag.CurrencyList = db.CurrencyLists.ToList();

                var currencies = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString()).ToList();
                return View(currencies);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult CurrencyView(Currencies currencies)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                //currencies.FirstCurrency = currencies.FirstCurrency;
                //currencies.SecondCurrency = currencies.SecondCurrency;
                //currencies.FirstCurrencyImage=
                currencies.Companyid = companyid.ToString();
                currencies.CreatedBy = name;
                currencies.CreatedTime = DateTime.Now.AddHours(5);
                currencies.ModifyBy = name;
                currencies.ModifyTime = DateTime.Now.AddHours(5);
                currencies.CreatedById = CreatedById;
                db.Currencies.Add(currencies);
                db.SaveChanges();
                TempData["UpdateCurrency"] = "Currecy added successfully.";
                return RedirectToAction("CurrencyView");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public ActionResult EditCurrency(int id)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                ViewBag.CurrencyList = db.CurrencyLists.ToList();
                var currencies = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.CurrenciesId == id).FirstOrDefault();
                return View(currencies);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult EditCurrency(Currencies cur)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                var currencies = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.CurrenciesId == cur.CurrenciesId).FirstOrDefault();
                //var findcurfromlist = db.CurrencyLists.Where(x => x.CurrencyName == cur.CurrencyList.CurrencyName).FirstOrDefault();
                if (currencies != null)
                {

                    currencies.FirstCurrency = cur.FirstCurrency;
                    currencies.SecondCurrency = cur.SecondCurrency;
                    if (cur.FirstCurrencyImage != null)
                    {
                        currencies.FirstCurrencyImage = cur.FirstCurrencyImage;
                    }
                    if (cur.SecondCurrencyImage != null)
                    {
                        currencies.SecondCurrencyImage = cur.SecondCurrencyImage;
                    }
                    currencies.CurrencyListId = cur.CurrencyListId;
                    currencies.ModifyBy = name;
                    currencies.CreatedById = CreatedById;
                    currencies.ModifyTime = DateTime.Now.AddHours(5);
                    db.Entry(currencies).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["UpdateCurrency"] = "Currency updated sucessfully.";
                }
                else
                {
                    ViewBag.mdg = "Currency not found.";
                }
                return RedirectToAction("CurrencyView");
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public JsonResult CheckCurrency(int id, int Curren)
        {
            string msg = "session";
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }

                if (id > 0)
                {
                    var findcurrency = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.CurrencyListId== Curren && x.CurrenciesId != id).FirstOrDefault();

                    if (findcurrency == null)
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
                    var findcurrency = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.CurrencyListId == Curren).FirstOrDefault();
                    if (findcurrency == null)
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
            else
            {
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ClientImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string subPath = "~/Images/Currency/";
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
    }
}