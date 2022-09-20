using AutoMapper;
using Risen.Contaxt;
using Risen.Models;
using Risen.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Risen.Controllers
{
    public class IdeasController : Controller
    {
        public static string SERVERAPIKEY = WebConfigurationManager.AppSettings["SERVER_API_KEY"];
        public static string SENDERID = WebConfigurationManager.AppSettings["SENDER_ID"];

        public IdeasController()
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<List<TradingSignals>, List<TradingSignalsDto>>();
            });
        }
        RisenContext db = new RisenContext();
        public async Task<ActionResult> TradingSignalView(string CurrencyName, string Status)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                string name = "";
                int? CreatedById = 0;
                var currencyList = db.Currencies.Where(x => x.Disable == false).ToList();
                ViewBag.currencieslist = currencyList;
                CompanyEmployee emp = new CompanyEmployee();

                ViewBag.employee = db.CompanyEmployees.Where(x => x.Enable == true).ToList();
                emp.fName = "All";
                emp.CompanyEmployeeID = 0;
                ViewBag.employee.Insert(0, emp);
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
                    var priviliges = Session["Priviliges"] as Privileges;
                    if (priviliges.isManager == false)
                    {
                        ViewBag.employee = db.CompanyEmployees.Where(x => x.CompanyEmployeeID == CreatedById).ToList();
                    }


                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

        }

        [HttpGet]
        public JsonResult TradingDataTable(DataTablesParam tablesParam)
        {
            List<TradingSignals> IdeasList = new List<TradingSignals>();
            //var IdeasList = (dynamic)null;
            var tradingSignalsDto = new List<TradingSignalsDto>();

            if (Session["Employee"] != null || Session["Company"] != null)
            {
                int companyid = 0;
                int? CreatedById = 0;
                IdeasList = db.TradingSignals.Include(x => x.CompanyEmployee).Include(x => x.CurrencyList).Where(x => x.Companyid == companyid.ToString()).OrderByDescending(x => x.TradingSignalId).ToList();
                if (Session["Company"] != null)
                {
                    RegisterComapany company = new RegisterComapany();
                    company = Session["Company"] as RegisterComapany;
                    companyid = company.RegisterComapanyID;
                    CreatedById = null;
                    if (!string.IsNullOrEmpty(tablesParam.Status) && tablesParam.Status == "0" || tablesParam.Status == "1" || tablesParam.Status == "2")
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.Disable == false && x.Status == tablesParam.Status).ToList();
                    else if (!string.IsNullOrEmpty(tablesParam.Status) && tablesParam.Status == "4" || tablesParam.Status == "5")
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.Disable == true && x.Status == tablesParam.Status).ToList();
                    if (!string.IsNullOrEmpty(tablesParam.CurrencyName))
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.CurrencyList.CurrencyName == tablesParam.CurrencyName).ToList();
                    if (tablesParam.EmployeeId > 0)
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.CreatedById == tablesParam.EmployeeId).OrderByDescending(x => x.TradingSignalId).ToList();
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    IdeasList = db.TradingSignals.Include(x => x.CompanyEmployee).Include(x => x.CurrencyList).Where(x => x.Companyid == companyid.ToString()).OrderByDescending(x => x.TradingSignalId).ToList();
                    CreatedById = employee1.CompanyEmployeeID;
                    if (!string.IsNullOrEmpty(tablesParam.Status) && tablesParam.Status == "0" || tablesParam.Status == "1" || tablesParam.Status == "2")
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.Disable == false && x.Status == tablesParam.Status).ToList();
                    else if (!string.IsNullOrEmpty(tablesParam.Status) && tablesParam.Status == "4" || tablesParam.Status == "5")
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.Disable == true && x.Status == tablesParam.Status).ToList();
                    if (!string.IsNullOrEmpty(tablesParam.CurrencyName))
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.CurrencyList.CurrencyName == tablesParam.CurrencyName).ToList();
                    if (tablesParam.EmployeeId > 0)
                        IdeasList = IdeasList.Where(x => x.Companyid == companyid.ToString() && x.CreatedById == tablesParam.EmployeeId).ToList();
                }
                int pageNo = 1;
                if (tablesParam.iDisplayStart >= tablesParam.iDisplayLength)
                {
                    pageNo = (tablesParam.iDisplayStart / tablesParam.iDisplayLength) + 1;
                }
                var IdeasListIdeasList = IdeasList.Skip((pageNo - 1) * tablesParam.iDisplayLength).Take(tablesParam.iDisplayLength).ToList();
                foreach (var item in IdeasListIdeasList)
                {
                    TradingSignalsDto signalsDto = new TradingSignalsDto();
                    signalsDto.TradingSignalId = item.TradingSignalId;
                    signalsDto.Buy = item.Buy;
                    signalsDto.CurrencyName = item.CurrencyList.CurrencyName;
                    signalsDto.Type = item.Type;
                    signalsDto.CreatedBy = item.CompanyEmployee.fName + " " + item.CompanyEmployee.lName;
                    signalsDto.Status = item.Status;
                    signalsDto.CurrentPrice = item.CurrentPrice;
                    signalsDto.CreatedTime = item.CreatedTime.ToString("dd-MM-yyyy HH:mm");
                    tradingSignalsDto.Add(signalsDto);
                }
                var returnData = new PaginatedResult<TradingSignalsDto>
                {
                    aaData = tradingSignalsDto,
                    iTotalRecords = IdeasList.Count(),
                    iTotalDisplayRecords = IdeasList.Count(),
                    sEcho = tablesParam.sEcho,
                    TotalCount = IdeasList.Count(),
                    i = 0,
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult EditTradingSignal(int id)
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
                var currency = db.Currencies.Where(x => x.Disable == false && x.Companyid == companyid.ToString()).ToList();
                ViewBag.Currencies = currency;
                var find = db.TradingSignals.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.TradingSignalId == id).FirstOrDefault();
                return View(find);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditTradingSignal(TradingSignals trading, List<string> TP, List<string> ProfitPIPS, string SL, string StopPIPS)
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
                if (TP == null || trading == null || ProfitPIPS == null || SL == null || SL == "" || StopPIPS == null || StopPIPS == "")
                {
                    TempData["msg"] = "empty";
                    return RedirectToAction("EditTradingSignal", routeValues: new { id = trading.TradingSignalId });
                }
                else
                {
                    var find = db.TradingSignals.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.TradingSignalId == trading.TradingSignalId).FirstOrDefault();
                    if (find != null)
                    {
                        find.Buy = trading.Buy;
                        find.CurrentPrice = trading.CurrentPrice;
                        find.Type = trading.Type;
                        find.CurrencyListId = trading.CurrencyListId;
                        find.Remarks = trading.Remarks;
                        find.ModifyBy = name;
                        //find.CreatedBy = name;
                        find.ModifyTime = DateTime.Now.AddHours(5);
                        find.ModifyBy = name;

                        if (trading.Image != null && trading.Image != "")
                        {
                            find.Image = trading.Image;
                        }

                        db.Entry(find).State = EntityState.Modified;
                        db.SaveChanges();
                        var query = db.TakeProfits.Where(x => x.TradingSignalId == trading.TradingSignalId);
                        // just get Un Achieved TP for ( delete and edit )
                        var findtp = query.Where(x => x.Disable == false).ToList();
                        // Achieved TP below
                        // Achieved TP must not be edited 
                        var findHitTp = query.Where(x => x.Disable == true).ToList();
                        foreach (var tp1 in findtp)
                        {
                            db.TakeProfits.Remove(tp1);
                            db.SaveChanges();
                        }
                        //add take profit
                        int j = 0;
                        for (var i = 0; i < TP.Count; i++)
                        {
                            if (TP[i] != "" && ProfitPIPS[i] != "")
                            {
                                TakeProfit takeProfit = new TakeProfit();

                                decimal tpValue = Convert.ToDecimal(TP[i]);
                                decimal BuyValue = Convert.ToDecimal(find.Buy);
                                string countPIPS = "";

                                if (find.Type == "1" || find.Type == "3" || find.Type == "6")
                                {
                                    // BuyValue is  a current value
                                    countPIPS = (tpValue - BuyValue).ToString();
                                }
                                if (find.Type == "2" || find.Type == "4" || find.Type == "5")
                                {
                                    countPIPS = (BuyValue - tpValue).ToString();
                                }
                                double cntPIPS = Convert.ToDouble(countPIPS);

                                takeProfit.TP = TP[i];
                                takeProfit.PIPS = Convert.ToInt32(cntPIPS).ToString();
                                takeProfit.TradingSignalId = trading.TradingSignalId;
                                takeProfit.Companyid = companyid.ToString();
                                takeProfit.CreatedBy = name;
                                takeProfit.CreatedTime = DateTime.Now.AddHours(5);
                                takeProfit.ModifyBy = name;
                                takeProfit.ModifyTime = DateTime.Now.AddHours(5);
                                takeProfit.CreatedById = CreatedById;
                                bool checkCont = (findHitTp.Count - 1) >= i ? true : false;
                                if (checkCont)
                                {
                                    if (findHitTp[i].No == j + 1)
                                    {
                                        j = j + 1;
                                    }
                                    else if (cntPIPS > 0)
                                    {
                                        j = j + 1;
                                        takeProfit.No = j;
                                        db.TakeProfits.Add(takeProfit);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (cntPIPS > 0)
                                    {
                                        j = j + 1;
                                        takeProfit.No = j;
                                        db.TakeProfits.Add(takeProfit);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        // end add take profit
                        var findsl = db.StopLoses.Where(x => x.TradingSignalId == trading.TradingSignalId).ToList();
                        foreach (var item in findsl)
                        {
                            db.StopLoses.Remove(item);
                            db.SaveChanges();
                        }
                        if (StopPIPS != "" && SL != "")
                        {
                            decimal slValue = Convert.ToDecimal(SL);
                            decimal BuyValue = Convert.ToDecimal(find.Buy);
                            string countPIPS = "";

                            // BuyValue is  a current value
                            if (find.Type == "1" || find.Type == "3" || find.Type == "6")
                            {
                                countPIPS = (slValue - BuyValue).ToString();
                            }
                            if (find.Type == "2" || find.Type == "4" || find.Type == "5")
                            {
                                countPIPS = (BuyValue - slValue).ToString();
                            }

                            double cntPIPS = Convert.ToDouble(countPIPS);

                            StopLose stopLose = new StopLose();
                            stopLose.SL = SL;
                            stopLose.PIPS = Convert.ToInt32(cntPIPS).ToString();
                            stopLose.TradingSignalId = trading.TradingSignalId;
                            stopLose.Companyid = companyid.ToString();
                            stopLose.CreatedBy = name;
                            stopLose.CreatedTime = DateTime.Now.AddHours(5);
                            stopLose.ModifyBy = name;
                            stopLose.ModifyTime = DateTime.Now.AddHours(5);
                            stopLose.CreatedById = CreatedById;
                            db.StopLoses.Add(stopLose);
                            db.SaveChanges();
                        }
                    }
                    //Update on Trade Idea - EUR/JPY
                    string body = "Update on Trade Idea -" + find.CurrencyList.CurrencyName + "by " + name;
                    int createdId = Convert.ToInt32(find.CreatedById);
                    if (find.Status == "1")
                    {
                        SendNotification(createdId, body, trading.TradingSignalId);
                    }
                    TempData["msg"] = "success";
                    TempData.Keep();
                    return RedirectToAction("TradingSignalView");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public ActionResult TakeProfit()
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
                var takeProfitList = db.TakeProfits.Where(x => x.Companyid == companyid.ToString()).ToList();
                return View(takeProfitList);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public JsonResult TakeProfit(string TP, string PIPS, int TradingSignalId)
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
                if (TP == "" || TP == null)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                if (PIPS == "" || TP == null)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                if (TradingSignalId <= 0)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                var findTradingSignal = db.TradingSignals.Where(x => x.TradingSignalId == TradingSignalId && x.Disable == false && x.Companyid == companyid.ToString()).FirstOrDefault();
                if (findTradingSignal == null)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                int countTP = db.TakeProfits.Where(x => x.TradingSignalId == TradingSignalId && x.Companyid == companyid.ToString() && x.Disable == false).Count();
                if (countTP >= 10)
                {
                    return Json("exist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    TakeProfit takeProfit = new TakeProfit();
                    takeProfit.TP = TP;
                    takeProfit.PIPS = PIPS;
                    takeProfit.TradingSignalId = TradingSignalId;
                    takeProfit.Companyid = companyid.ToString();
                    takeProfit.CreatedBy = name;
                    takeProfit.CreatedTime = DateTime.Now.AddHours(5);
                    takeProfit.ModifyBy = name;
                    takeProfit.ModifyTime = DateTime.Now.AddHours(5);
                    takeProfit.CreatedById = CreatedById;
                    db.TakeProfits.Add(takeProfit);
                    db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult StopLoss(string SL, string PIPS, int TradingSignalId)
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
                if (string.IsNullOrEmpty(SL))
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(PIPS))
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                if (TradingSignalId <= 0)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                var findTradingSignal = db.TradingSignals.Where(x => x.TradingSignalId == TradingSignalId && x.Disable == false && x.Companyid == companyid.ToString()).FirstOrDefault();
                if (findTradingSignal == null)
                {

                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                var findsl = db.StopLoses.Where(x => x.TradingSignalId == TradingSignalId && x.Companyid == companyid.ToString() && x.Disable == false).FirstOrDefault();
                if (findsl == null)
                {
                    StopLose stopLose = new StopLose();
                    stopLose.SL = SL;
                    stopLose.PIPS = PIPS;
                    stopLose.TradingSignalId = TradingSignalId;
                    stopLose.Companyid = companyid.ToString();
                    stopLose.CreatedBy = name;
                    stopLose.CreatedTime = DateTime.Now.AddHours(5);
                    stopLose.ModifyBy = name;
                    stopLose.ModifyTime = DateTime.Now.AddHours(5);
                    stopLose.CreatedById = CreatedById;
                    db.StopLoses.Add(stopLose);
                    db.SaveChanges();
                }
                else
                {
                    return Json("exist", JsonRequestBehavior.AllowGet);
                }

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetProfitLost(int TradingSignalId)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                TradingSignalViewModel TradingList = new TradingSignalViewModel();
                List<TakeProfitViewModel> TPList = new List<TakeProfitViewModel>();
                List<StopLoseViewModel> SLList = new List<StopLoseViewModel>();

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
                TradingSignalViewModel tradingSignalViewModel = new TradingSignalViewModel();
                var tradingObj = db.TradingSignals.Where(x =>x.Companyid == companyid.ToString() && x.TradingSignalId == TradingSignalId).FirstOrDefault();
                if (tradingObj == null)
                {
                    return Json("NoData", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tradingSignalViewModel = new TradingSignalViewModel();
                    tradingSignalViewModel.TradingSignalId = tradingObj.TradingSignalId;
                    tradingSignalViewModel.CurrencyListId = tradingObj.CurrencyListId;
                    //var currency = db.Currencies.Where(x => x.Disable == false && x.CurrenciesId == tradingObj.CurrenciesId && x.Companyid == companyid.ToString()).FirstOrDefault();
                    tradingSignalViewModel.CurrencyName = tradingObj.CurrencyList.CurrencyName;
                    tradingSignalViewModel.Buy = tradingObj.Buy;
                    tradingSignalViewModel.Remarks = tradingObj.Remarks;
                    tradingSignalViewModel.CreatedByImageImage = "Images/Trading/" + tradingObj.Image;
                    tradingSignalViewModel.CurrentPrice = tradingObj.CurrentPrice;
                    tradingSignalViewModel.Points = tradingObj.PIPS;
                    tradingSignalViewModel.CreatedBy = tradingObj.CreatedBy;
                    tradingSignalViewModel.CreatedTime = tradingObj.CreatedTime.ToString("dd/MM/yyy HH:mm");
                    tradingSignalViewModel.Companyid = tradingObj.Companyid;
                    tradingSignalViewModel.CurrencyImage = tradingObj.FromImage;
                    tradingSignalViewModel.CreatedBy = tradingObj.CreatedBy;
                    

                    // Take Profit List 
                    var takeProfitList = db.TakeProfits.Where(x => x.TradingSignalId == tradingObj.TradingSignalId && x.Companyid == companyid.ToString()).ToList();
                    foreach (var meta in takeProfitList)
                    {
                        TakeProfitViewModel TPobject = new TakeProfitViewModel();
                        TPobject.TakeProfitId = meta.TakeProfitId;
                        TPobject.TradingSignalId = meta.TradingSignalId;
                        TPobject.Points = meta.PIPS.ToString();
                        TPobject.TP = meta.TP;
                        TPobject.Disble = meta.Disable;

                        TPList.Add(TPobject);
                    }

                     // End take profit list
                    //start Stop Lose
                    var StopLoseList = db.StopLoses.Where(x => x.TradingSignalId == tradingObj.TradingSignalId && x.Companyid == companyid.ToString()).FirstOrDefault();
                    StopLoseViewModel SLobject = new StopLoseViewModel();
                    if (StopLoseList != null)
                    {
                        SLobject = new StopLoseViewModel();
                        SLobject.StopLoseId = StopLoseList.StopLoseId;
                        SLobject.TradingSignalId = StopLoseList.TradingSignalId;
                        SLobject.Points = StopLoseList.PIPS.ToString();
                        SLobject.SL = StopLoseList.SL;
                    }

                    tradingSignalViewModel.TakeProfitList = TPList;
                    tradingSignalViewModel.StopLose = SLobject;
                }
                return Json(tradingSignalViewModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Expire", JsonRequestBehavior.AllowGet);
            }
        }

       
        [HttpGet]
        public ActionResult TradingDetail(int TradingSignalId)
        {
            if (Session["Employee"] != null || Session["Company"] != null)
            {
                List<TakeProfitViewModel> TPList = new List<TakeProfitViewModel>();
                List<StopLoseViewModel> SLList = new List<StopLoseViewModel>();
                TradingDTO trading = new TradingDTO();

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

                TradingSignalViewModel tradingSignalViewModel = new TradingSignalViewModel();
                var tradingObj = db.TradingSignals.Where(x => x.Disable == false && x.Companyid == companyid.ToString() && x.TradingSignalId == TradingSignalId).FirstOrDefault();
                if (tradingObj == null)
                {
                    return RedirectToAction("TradingSignalView");
                }
                else
                {
                    tradingSignalViewModel = new TradingSignalViewModel();
                    tradingSignalViewModel.TradingSignalId = tradingObj.TradingSignalId;
                    tradingSignalViewModel.CurrencyListId = tradingObj.CurrencyListId;
                    //var currency = db.Currencies.Where(x => x.Disable == false && x.CurrenciesId == tradingObj.CurrenciesId && x.Companyid == companyid.ToString()).FirstOrDefault();
                    tradingSignalViewModel.CurrencyName = tradingObj.CurrencyList.CurrencyName;
                    tradingSignalViewModel.Buy = tradingObj.Buy;
                    tradingSignalViewModel.Remarks = tradingObj.Remarks;
                    tradingSignalViewModel.CreatedByImageImage = "Images/Trading/" + tradingObj.Image;
                    tradingSignalViewModel.CurrentPrice = tradingObj.CurrentPrice;
                    tradingSignalViewModel.Points = tradingObj.PIPS;
                    tradingSignalViewModel.CreatedBy = tradingObj.CreatedBy;
                    tradingSignalViewModel.CreatedTime = tradingObj.CreatedTime.ToString("dd/MM/yyy HH:mm");
                    tradingSignalViewModel.Companyid = tradingObj.Companyid;
                    tradingSignalViewModel.CurrencyImage = tradingObj.FromImage;
                    tradingSignalViewModel.CreatedBy = tradingObj.CreatedBy;

                    // Take Profit List 
                    var takeProfitList = db.TakeProfits.Where(x => x.Disable == false && x.TradingSignalId == tradingObj.TradingSignalId && x.Companyid == companyid.ToString()).ToList();
                    foreach (var meta in takeProfitList)
                    {
                        TakeProfitViewModel TPobject = new TakeProfitViewModel();
                        TPobject.TakeProfitId = meta.TakeProfitId;
                        TPobject.TradingSignalId = meta.TradingSignalId;
                        TPobject.Points = meta.PIPS.ToString();
                        TPobject.TP = meta.TP;
                        TPList.Add(TPobject);
                    }

                    // End take profit list
                    //start Stop Lose
                    var StopLoseobj = db.StopLoses.Where(x => x.Disable == false && x.TradingSignalId == tradingObj.TradingSignalId && x.Companyid == companyid.ToString()).FirstOrDefault();

                    StopLoseViewModel SLobject = new StopLoseViewModel();
                    SLobject.StopLoseId = StopLoseobj.StopLoseId;
                    SLobject.TradingSignalId = StopLoseobj.TradingSignalId;
                    SLobject.Points = StopLoseobj.PIPS.ToString();
                    SLobject.SL = StopLoseobj.SL;


                    tradingSignalViewModel.TakeProfitList = TPList;
                    tradingSignalViewModel.StopLose = SLobject;
                }

                return View(tradingSignalViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int TradingSignalId, string status)
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
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                }
                if (TradingSignalId <= 0)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                var findTradingSignal = db.TradingSignals.Where(x => x.TradingSignalId == TradingSignalId && x.Disable == false && x.Companyid == companyid.ToString()).FirstOrDefault();
                if (findTradingSignal == null)
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
                findTradingSignal.Status = status;
                findTradingSignal.Companyid = companyid.ToString();
                findTradingSignal.ModifyBy = name;
                findTradingSignal.ModifyTime = DateTime.Now.AddHours(5);
                db.Entry(findTradingSignal).State = EntityState.Modified;
                db.SaveChanges();
                if (status == "1" || status == "2" || status == "0")
                {
                    Notification notification = new Notification();
                    notification.RegisterComapanyID = companyid;
                    notification.Title = "Treading Idea";
                    notification.CreatedTime = DateTime.Now.AddHours(5);
                    notification.CurrencyName = findTradingSignal.CurrencyList.firstImage;
                    notification.EmployeeName = findTradingSignal.CreatedBy;
                    notification.CreatedById = CreatedById;
                    notification.CompanyEmployeeID = CreatedById;
                    notification.Price = findTradingSignal.Buy;
                    if (findTradingSignal.Type == "1")
                    {
                        notification.Type = "1";
                    }
                    else if (findTradingSignal.Type == "2")
                    {
                        notification.Type = "2";
                    }
                    notification.Price = findTradingSignal.Buy;
                    if (status == "1")
                    {
                         //for live to go pending : Trade Idea Withdrawn - USD / JPY by Educator Name
                        //For the above pending one to go back to active : Trade Idea Updated - USD / JPY by Educator Name
                        
                        var modifytime = findTradingSignal.ModifyTime.ToString("dd-MM-yyyy hh:mm");
                        var createdTime = findTradingSignal.CreatedTime.ToString("dd-MM-yyyy hh:mm");
                        if (modifytime == createdTime)
                        {
                            notification.Body = "New Trade Idea - " + notification.CurrencyName + " by " + name;
                        }
                        else
                        {
                            notification.Body = "Trade Idea Updated - " + notification.CurrencyName + " by " + name;
                        }
                        //New Trade Idea - EUR/JPY
                        notification.Status = "1";
                    }
                    else if (status == "2")
                    {
                        notification.Body = "Trade Idea Blocked - " + notification.CurrencyName + " by " + name;
                        notification.Status = "2";
                    }
                    else if (status == "0")
                    {
                        notification.Body = "Trade Idea Withdrawn - " + notification.CurrencyName + " by " + name;
                        notification.Status = "0";
                    }
                    findTradingSignal.ModifyTime = DateTime.Now.AddHours(5);
                    db.Entry(findTradingSignal).State = EntityState.Modified;
                    db.SaveChanges();

                    notification.TradingSignalId = findTradingSignal.TradingSignalId.ToString();
                    db.Notifications.Add(notification);
                    db.SaveChanges();

                    int employeeid = Convert.ToInt32(findTradingSignal.CreatedById);
                    SendNotification(employeeid, notification.Body ,findTradingSignal.TradingSignalId);
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("expire", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TopPips()
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
                    var findEmployeePips = db.EmployeePIPs.Where(x => x.Disable == false && x.CompanyId == companyid.ToString()).GroupBy(x => x.EmployeePIPSId).Count();
                    var IdeasList = db.TradingSignals.Where(x => x.Disable == false && x.Companyid == companyid.ToString()).ToList().OrderByDescending(x => x.TradingSignalId).ToList();
                    return View(IdeasList);
                }
                else
                {
                    CompanyEmployee employee1 = new CompanyEmployee();
                    employee1 = Session["Employee"] as CompanyEmployee;
                    companyid = employee1.Companyid;
                    name = employee1.fName + " " + employee1.lName;
                    CreatedById = employee1.CompanyEmployeeID;
                    if (employee1.Designation.Name == "Admin")
                    {
                        var findEmployeePips = db.EmployeePIPs.Where(x => x.Disable == false && x.CompanyId == employee1.CompanyEmployeeID.ToString()).GroupBy(x => x.EmployeePIPSId).Count();
                        var IdeasList = db.TradingSignals.Where(x => x.Disable == false && x.Companyid == companyid.ToString()).ToList().OrderByDescending(x => x.TradingSignalId).ToList();
                        return View(IdeasList);
                    }
                    else
                    {
                        var IdeasList = db.TradingSignals.Where(x => x.Disable == false && x.CreatedById == employee1.CompanyEmployeeID && x.Companyid == companyid.ToString()).ToList().OrderByDescending(x => x.TradingSignalId).ToList();
                        return View(IdeasList);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        public async void SendNotification(int employeeId, string body , int tradeId)
        {
            try
            {
                List<int> userid = db.AllowNotifications.Where(u => u.CompanyEmployeeID == employeeId).Select(u => u.DeviceUserId).ToList();

                var user = db.Users.ToList();

                List<string> singlebatch = new List<string>();
                for (int i = 0; i < user.Count; i++)
                {
                    bool result = true;
                    for (int j = 0; j < userid.Count; j++)
                    {
                        if (user[i].DeviceUserId == userid[j])
                        {
                            result = false;
                            break;
                        }
                    }
                    if (result)
                    {
                        singlebatch.Add(user[i].DeviceToken);
                    }
                }

                if (singlebatch.Count > 0)
                {
                    dynamic data = new
                    {
                        registration_ids = singlebatch,
                        notification = new
                        {
                            title = "Trade Idea",     // Notification title
                            body = body,             // Notification body data
                            link = "--link--",      // When click on notification user redirect to this link
                            tradeId = tradeId,
                            sound="bing"
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
    }
}