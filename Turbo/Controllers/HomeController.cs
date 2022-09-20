using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;
using Risen.Contaxt;
using Risen.Models;
using Risen.ViewModel;

namespace Risen.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    //<pages enableSessionState = "true" >
    public class HomeController : Controller
    {
        RisenContext db = new RisenContext();

        public static string SERVERAPIKEY = WebConfigurationManager.AppSettings["SERVER_API_KEY"];
        public static string SENDERID = WebConfigurationManager.AppSettings["SENDER_ID"];

        public ActionResult Index()
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {

                List<int> monthlyCount = new List<int>();
                List<int> dailyCount = new List<int>();

                DateTime currenctdate = DateTime.Now.AddHours(5);
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;

                int CountEmployees = 0;
                int CountTradingIdeas = 0;
                int CountCurrencies = 0;
                DashboardVM dashbaord = new DashboardVM();
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = null;
                    var tradingSignals = db.TradingSignals.Where(x => x.Companyid == companyid.ToString()).ToList();
                    CountTradingIdeas = tradingSignals.Count();
                    dashbaord.TradingIdeas = CountTradingIdeas;
                    ViewBag.TradingSignal = tradingSignals.Take(10).ToList();
                    // Barchart data
                    for (int i = 1; i < 13; i++)
                    {
                        int totalday = DateTime.DaysInMonth(currenctdate.Year, i);
                        DateTime firstDay = new DateTime(currenctdate.Year, i, 1);
                        DateTime lastDay = new DateTime(currenctdate.Year, i, totalday);
                        CountTradingIdeas = 0;
                        CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date <= lastDay.Date && x.CreatedTime.Date >= firstDay.Date).Count();
                        monthlyCount.Add(CountTradingIdeas);
                        dashbaord.mountCount = monthlyCount;
                        ViewBag.Incomlist = monthlyCount;
                    }
                    // barchart data end

                    // daily bar cahrt
                    int totaldayInmonth = DateTime.DaysInMonth(currenctdate.Year, currenctdate.Month);
                    for (int j = 1; j <= totaldayInmonth; j++)
                    {
                        DateTime Day = new DateTime(currenctdate.Year, currenctdate.Month, j);
                        CountTradingIdeas = 0;
                        CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date == Day.Date).Count();
                        dailyCount.Add(CountTradingIdeas);
                        //ViewBag.daily = dailyCount;
                        dashbaord.dailyCount = dailyCount;
                    }
                    //
                }
                else
                {
                    // Employee

                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    var privileges = Session["Priviliges"] as Privileges;
                    //employee1 = TempData["Employee"] as CompanyEmployee;

                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                    if (employee1.Designation.Name == "Admin" || privileges.isManager == true)
                    {
                        var tradingSignals = db.TradingSignals.Include("CompanyEmployee").Where(x => x.Companyid == companyid.ToString()).Take(30).ToList();
                        CountTradingIdeas = tradingSignals.Count();
                        dashbaord.TradingIdeas = tradingSignals.Count();
                        ViewBag.TradingSignal = tradingSignals.Take(10).ToList();

                        // Barchart data for admin
                        for (int i = 1; i < 13; i++)
                        {
                            int totalday = DateTime.DaysInMonth(currenctdate.Year, i);
                            DateTime firstDay = new DateTime(currenctdate.Year, i, 1);
                            DateTime lastDay = new DateTime(currenctdate.Year, i, totalday);
                            CountTradingIdeas = 0;
                            CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date <= lastDay.Date && x.CreatedTime.Date >= firstDay.Date).Count();
                            monthlyCount.Add(CountTradingIdeas);
                            ViewBag.Incomlist = monthlyCount;
                        }

                        // barchart data end
                        // daily bar cahrt for admin
                        int totaldayInmonth = DateTime.DaysInMonth(currenctdate.Year, currenctdate.Month);
                        for (int j = 1; j <= totaldayInmonth; j++)
                        {
                            DateTime Day = new DateTime(currenctdate.Year, currenctdate.Month, j);
                            CountTradingIdeas = 0;
                            CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date == Day.Date).Count();
                            dailyCount.Add(CountTradingIdeas);
                            //ViewBag.daily = dailyCount;
                            dashbaord.dailyCount = dailyCount;
                        }
                    }
                    else
                    {
                        var tradingSignals = db.TradingSignals.Where(x => x.Companyid == companyid.ToString() && x.CreatedById == employee1.CompanyEmployeeID).ToList();
                        dashbaord.TradingIdeas = CountTradingIdeas;
                        ViewBag.TradingSignal = tradingSignals.Take(10).ToList();

                        // monthly barchrt data
                        for (int i = 1; i < 13; i++)
                        {
                            int totalday = DateTime.DaysInMonth(currenctdate.Year, i);
                            DateTime firstDay = new DateTime(currenctdate.Year, i, 1);
                            DateTime lastDay = new DateTime(currenctdate.Year, i, totalday);

                            CountTradingIdeas = 0;
                            CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date <= lastDay.Date && x.CreatedTime.Date >= firstDay.Date).Count();
                            monthlyCount.Add(CountTradingIdeas);
                            ViewBag.Incomlist = monthlyCount;
                            dashbaord.mountCount = monthlyCount;
                        }
                        // monthly barchart data end

                        // daily bar cahrt
                        int totaldayInmonth = DateTime.DaysInMonth(currenctdate.Year, currenctdate.Month);
                        for (int j = 1; j <= totaldayInmonth; j++)
                        {
                            DateTime Day = new DateTime(currenctdate.Year, currenctdate.Month, j);
                            CountTradingIdeas = 0;
                            CountTradingIdeas = tradingSignals.Where(x => x.CreatedTime.Date == Day.Date).Count();
                            dailyCount.Add(CountTradingIdeas);
                            //ViewBag.daily = dailyCount;
                            dashbaord.dailyCount = dailyCount;
                        }
                        // end daily barchart
                    }
                }
                CountCurrencies = db.Currencies.Where(x => x.Companyid == companyid.ToString() && x.Disable == false).Count();
                dashbaord.Currencies = CountCurrencies;
                CountEmployees = db.CompanyEmployees.Where(x => x.Companyid == companyid && x.Enable == true).Count();
                dashbaord.Employees = CountEmployees;
                //List<string> months = new List<string>();

                return View(dashbaord);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpGet]
        public ActionResult Dashbaord()
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
                ViewBag.Currencies = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString()).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        [HttpPost]
        public ActionResult Dashbaord(TradingSignals tradingSignals, List<string> TP, List<string> ProfitPIPS, string SL, string StopPIPS)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int CreatedById = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    name = company.Name;
                    CreatedById = 0;
                }
                else
                {
                    // Type
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                if (tradingSignals.CurrencyListId <= 0)
                {
                    ViewBag.msg = "Please select currency";
                    return View();
                }
                else
                {
                    tradingSignals.Companyid = companyid.ToString();
                    tradingSignals.CreatedBy = name;
                    tradingSignals.CreatedById = CreatedById;
                    tradingSignals.CreatedTime = DateTime.Now.AddHours(5);
                    tradingSignals.ModifyTime = DateTime.Now.AddHours(5);
                    tradingSignals.Status = "0";
                    tradingSignals.CompanyEmployeeID = CreatedById;


                    db.TradingSignals.Add(tradingSignals);
                    db.SaveChanges();
                    TempData["Success"] = "New Idea successfully";
                    var findCUrrency = db.CurrencyLists.Where(x => x.CurrencyListId == tradingSignals.CurrencyListId).FirstOrDefault();
                    //
                    if (StopPIPS != "" && SL != "")
                    {
                        decimal slValue = Convert.ToDecimal(SL);
                        decimal BuyValue = Convert.ToDecimal(tradingSignals.Buy);
                        string countPIPS = "";

                        // BuyValue is  a current value
                        if (tradingSignals.Type == "1" || tradingSignals.Type == "3" || tradingSignals.Type == "6")
                        {
                            countPIPS = (slValue - BuyValue).ToString();
                        }
                        if (tradingSignals.Type == "2" || tradingSignals.Type == "4" || tradingSignals.Type == "5")
                        {
                            countPIPS = (BuyValue - slValue).ToString();
                        }

                        double cntPIPS = Convert.ToDouble(countPIPS);


                        StopLose stopLose = new StopLose();
                        stopLose.SL = SL;
                        stopLose.PIPS = Convert.ToInt32(cntPIPS).ToString();
                        stopLose.TradingSignalId = tradingSignals.TradingSignalId;
                        stopLose.Companyid = companyid.ToString();
                        stopLose.CreatedBy = name;
                        stopLose.CreatedTime = DateTime.Now.AddHours(5);
                        stopLose.ModifyBy = name;
                        stopLose.ModifyTime = DateTime.Now.AddHours(5);
                        stopLose.CreatedById = CreatedById;
                        db.StopLoses.Add(stopLose);
                        db.SaveChanges();
                    }
                    //
                    //add take profit
                    int j = 0;
                    for (var i = 0; i < TP.Count; i++)
                    {
                        if (TP[i] != "" && ProfitPIPS[i] != "")
                        {
                            TakeProfit takeProfit = new TakeProfit();

                            decimal tpValue = Convert.ToDecimal(TP[i]);
                            decimal BuyValue = Convert.ToDecimal(tradingSignals.Buy);
                            string countPIPS = "";

                            if (tradingSignals.Type == "1" || tradingSignals.Type == "3" || tradingSignals.Type == "6")
                            {
                                // BuyValue is  a current value
                                countPIPS = (tpValue - BuyValue).ToString();
                            }
                            if (tradingSignals.Type == "2" || tradingSignals.Type == "4" || tradingSignals.Type == "5")
                            {
                                countPIPS = (BuyValue - tpValue).ToString();
                            }

                            double cntPIPS = Convert.ToDouble(countPIPS);
                            takeProfit.TP = TP[i];
                            takeProfit.PIPS = Convert.ToInt32(cntPIPS).ToString();
                            takeProfit.TradingSignalId = tradingSignals.TradingSignalId;
                            takeProfit.Companyid = companyid.ToString();
                            takeProfit.CreatedBy = name;
                            takeProfit.CreatedTime = DateTime.Now.AddHours(5);
                            takeProfit.ModifyBy = name;
                            takeProfit.ModifyTime = DateTime.Now.AddHours(5);
                            takeProfit.CreatedById = CreatedById;
                            if (cntPIPS > 0)
                            {
                                j = j + 1;
                                takeProfit.No = j;
                                db.TakeProfits.Add(takeProfit);
                                db.SaveChanges();
                            }
                        }
                    }
                    // end add take profit
                    return RedirectToAction("TradingSignalView", "Ideas");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
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
                string subPath = "~/Images/Trading/";
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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult ChartImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string subPath = "~/Images/Charts/";
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

        public JsonResult GetNotification()
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
                    // Type
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                List<NotificationViewModel> notificationList = new List<NotificationViewModel>();
                var findNotifications = db.Notifications.Where(x => x.RegisterComapanyID == companyid && x.CreatedById == CreatedById).OrderByDescending(x => x.NotificationId).ToList();
                if (findNotifications.Count > 0)
                {
                    foreach (var item in findNotifications)
                    {
                        NotificationViewModel notificationVM = new NotificationViewModel();
                        notificationVM.title = item.Title;
                        notificationVM.body = item.Body;
                        notificationVM.createdTime = item.CreatedTime.ToString();
                        notificationVM.companyId = item.RegisterComapanyID;
                        notificationVM.currencyName = item.CurrencyName;
                        notificationVM.employeeName = item.EmployeeName;
                        notificationVM.TradingSignalId = item.TradingSignalId;
                        if (item.Type == "1")
                        {
                            notificationVM.type = "Buy";
                        }
                        else if (item.Type == "2")
                        {
                            notificationVM.type = "Sell";
                        }
                        notificationVM.price = item.Price;
                        if (item.Status == "0")
                        {
                            notificationVM.status = "Pending";
                        }
                        else if (item.Status == "1")
                        {
                            notificationVM.status = "Approved";
                        }
                        else if (item.Status == "2")
                        {
                            notificationVM.status = "Rejected";
                        }

                        notificationList.Add(notificationVM);
                    }
                }
                return Json(notificationList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("success", JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult CustomeImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string subPath = "~/Images/Notifications/";
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

        [HttpPost]
        public async Task<JsonResult> SaveCustomeNotification(string title, string NotBody, string notImage)
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
                    // Type
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }

                Notification notification = new Notification();
                notification.RegisterComapanyID = companyid;
                notification.Title = title;
                notification.CreatedTime = DateTime.Now.AddHours(5);
                notification.Body = NotBody;
                notification.EmployeeName = name;
                notification.CreatedById = Convert.ToInt32(CreatedById);
                notification.CompanyEmployee.CompanyEmployeeID = Convert.ToInt32(CreatedById);
                notification.Type = "custome";
                notification.Status = notImage;
                db.Notifications.Add(notification);
                db.SaveChanges();
                SendCustomNotification(NotBody, title, notImage);


                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);

            }
        }


        public async void SendCustomNotification(string body, string title, string image)
        {
            try
            {
                var user = db.Users.Select(x=>x.DeviceToken).ToList();
               
                if (user.Count > 0)
                {
                    dynamic data = new
                    {
                        registration_ids = user,
                        // this is for multiple user // registration_ids = singlebatch, // this is for multiple user                                                                                                                                                                          // registration_ids = singlebatch, // this is for multiple user 
                        notification = new
                        {
                            title = title,            // Notification title
                            body = body,             // Notification body data
                            link = "custome",       // When click on notification user redirect to this link
                            image = "/Images/Notifications/" + image,
                            sound = "bing"
                        },
                        aps = new
                        {
                            badge = 0,
                            sound = "bingbong.aiff"
                        },
                        messageID = "ABCDEFIGHIJ"
                    };

                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var json = serializer.Serialize(data);
                    Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);

                    string SERVER_API_KEY = SERVERAPIKEY;
                    string SENDER_ID = SENDERID;


                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                    tRequest.ContentLength = byteArray.Length;
                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse tResponse = tRequest.GetResponse();
                    dataStream = tResponse.GetResponseStream();

                    StreamReader tReader = new StreamReader(dataStream);

                    String sResponseFromServer = tReader.ReadToEnd();

                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCurrencyName(int CurrencyId)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                }
                else
                {
                    // Type
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;

                }
                var findCurrency = db.CurrencyLists.Where(x=>x.CurrencyListId==CurrencyId).FirstOrDefault();
                return Json(findCurrency.CurrencyName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);

            }
        }



    }
}