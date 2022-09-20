using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using Risen.Contaxt;
using Risen.ViewModel;

namespace Risen.Models
{
    public class Jobclass : IJob
    {
        public static string SERVERAPIKEY = WebConfigurationManager.AppSettings["SERVER_API_KEY"];
        public static string SENDERID = WebConfigurationManager.AppSettings["SENDER_ID"];
        public static string CurrencyApiKey = WebConfigurationManager.AppSettings["CurrencyApiKey"];
        RisenContext db = new RisenContext();
        Task IJob.Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                BackgroundService();
            });
        }


        public  bool BackgroundService()
        {
            var tradeideas = db.TradingSignals.Include(x => x.CompanyEmployee).Where(x => x.Status == "1" && x.CompanyEmployee.IsHide == false).ToList();
            foreach (var trad in tradeideas)
            {
                string currenyPair = trad.CurrencyList.CurrencyName.Replace(@"/", string.Empty);
                string Baseurl = "https://api.finage.co.uk/last/forex/";
                decimal currentRate = 0;
                string body = "";
                string currencyname = trad.CurrencyList.CurrencyName;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                currencyname = currencyname.Substring(currencyname.Length - 3);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync(currenyPair + "?apikey=" + CurrencyApiKey);
                    Res.Wait();
                    var result = Res.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var EmpResponse = result.Content.ReadAsStringAsync().Result;
                        var obj = JsonConvert.DeserializeObject(EmpResponse);
                        currencyJson user = jss.Deserialize<currencyJson>(obj.ToString());
                        int i = 0;
                        decimal countPIPS = 0;
                        countPIPS = Convert.ToDecimal(trad.Buy) - currentRate;
                        if (trad.Type == "1" || trad.Type == "3" || trad.Type == "6")
                        {
                            countPIPS = Convert.ToDecimal(trad.Buy) > currentRate ? System.Math.Abs(countPIPS) * (-1) : System.Math.Abs(countPIPS);
                        }
                        if (trad.Type == "2" || trad.Type == "4" || trad.Type == "5")
                        {
                            countPIPS = currentRate > Convert.ToDecimal(trad.Buy) ? System.Math.Abs(countPIPS) * (-1) : System.Math.Abs(countPIPS);
                        }
                        var takeProfit = db.TakeProfits.Where(x => x.Disable == false && x.TradingSignalId == trad.TradingSignalId && x.Companyid == trad.Companyid).ToList();
                        foreach (var tpItem in takeProfit)
                        {
                            i = i + 1;
                            if (countPIPS >= Convert.ToDecimal(tpItem.PIPS))
                            {
                                //Take Profit 3 Achieved +100 PIPS - EUR/JPY
                                body = "Take Profit " + tpItem.No + " Achieved +" + tpItem.PIPS + " Points - " + trad.CurrencyList.firstImage;
                                // disable is true because this will not show in pass result , these records will only show in past result when the idea is completely won or loss
                                double pipsResult = Convert.ToDouble(tpItem.PIPS);
                                // change tP status
                                tpItem.Disable = true;
                                tpItem.ModifyTime = DateTime.Now;
                                db.Entry(tpItem).State = EntityState.Modified;
                                db.SaveChanges();
                                SaveNotification(trad, body, "5");
                                SendNotification(Convert.ToInt32(trad.CreatedById), body, trad.TradingSignalId);
                            }
                        }
                        // when all tp are hitted then end the trade idea
                        var findTakePrfit = db.TakeProfits.Where(x => x.Disable == false && x.TradingSignalId == trad.TradingSignalId && x.Companyid == trad.Companyid).ToList();
                        if (findTakePrfit.Count() == 0)
                        {
                            var tpend = db.TakeProfits.Where(x => x.TradingSignalId == trad.TradingSignalId).OrderByDescending(x => x.PIPS).FirstOrDefault();
                            double pipsResult = Convert.ToDouble(tpend.PIPS);
                            SaveEmployeePIPS(trad, "won", Convert.ToInt32(pipsResult));
                            body = "Trade Ended & Idea Won - " + trad.CurrencyList.firstImage;
                            trad.Disable = true;
                            trad.Status = "5";
                            db.Entry(trad).State = EntityState.Modified;
                            db.SaveChanges();
                            SaveNotification(trad, body, trad.Status);
                            SendNotification(Convert.ToInt32(trad.CreatedById), body, trad.TradingSignalId);
                            break;
                        }
                        var loss = db.StopLoses.Where(x => x.Disable == false && x.TradingSignalId == trad.TradingSignalId).FirstOrDefault();
                        if (loss != null)
                        {
                            double pipsResult = Convert.ToDouble(loss.PIPS);
                            if (countPIPS <= Convert.ToDecimal(pipsResult))
                            {
                                //SL HIT unfortunately - 30 PIPS - EUR / JPY
                                loss.Disable = true;
                                loss.ModifyTime = DateTime.Now;
                                db.Entry(loss).State = EntityState.Modified;
                                db.SaveChanges();
                                // trade idea end 
                                trad.Disable = true;
                                // if take profit is null or count =0 then idea shuld be loss else idea shuld be won
                                var takeprofit = db.TakeProfits.Where(x => x.Disable == true && x.TradingSignalId == trad.TradingSignalId && x.Companyid == trad.Companyid).ToList();
                                if (takeprofit.Count > 0)
                                {
                                    trad.Status = "5";
                                    body = "Trade Ended  & Idea Won - " + trad.CurrencyList.firstImage;
                                    var tpHit = takeProfit.OrderByDescending(x => x.PIPS).FirstOrDefault();
                                    SaveEmployeePIPS(trad, "won", Convert.ToInt32(tpHit.PIPS));
                                }
                                else
                                {
                                    trad.Status = "4";
                                    body = "SL HIT unfortunately " + pipsResult + " Points - " + trad.CurrencyList.firstImage;
                                    SaveEmployeePIPS(trad, "loss", Convert.ToInt32(pipsResult));
                                }
                                db.Entry(trad).State = EntityState.Modified;
                                db.SaveChanges();
                                SaveNotification(trad, body, trad.Status);
                                SendNotification(Convert.ToInt32(trad.CreatedById), body, trad.TradingSignalId);
                            }
                        }
                    }
                }
            }
            return true;
        }

        public async void SendNotification(int employeeId, string body, int tradeId)
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
                            title = "Trade Idea",      // Notification title
                            body = body,              // Notification body data
                            link = "--link--",      // When click on notification user redirect to this link
                            tradeId = tradeId,
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

        public async void SaveNotification(TradingSignals trad, string body, string status)
        {
            Notification notification = new Notification();
            notification.RegisterComapanyID = Convert.ToInt32(trad.Companyid);
            notification.Title = "Trade Idea";
            notification.CreatedTime = DateTime.Now;
            notification.CurrencyName = trad.CurrencyList.firstImage;
            notification.EmployeeName = trad.CompanyEmployee.fName + " " + trad.CompanyEmployee.lName;
            notification.Type = trad.Type;
            notification.Body = body;
            notification.TradingSignalId = trad.TradingSignalId.ToString();
            notification.CreatedById = Convert.ToInt32(trad.CreatedById);
            notification.Status = status;
            notification.CompanyEmployeeID = Convert.ToInt32(trad.CreatedById);
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public async void SaveEmployeePIPS(TradingSignals trad, string status, int tpHit)
        {
            EmployeePIPS employeePIPS = new EmployeePIPS();
            employeePIPS.TradingSignalId = trad.TradingSignalId;
            employeePIPS.CompanyEmployeeID = Convert.ToInt32(trad.CreatedById);
            employeePIPS.CompanyId = trad.Companyid.ToString();
            employeePIPS.CreatedTime = DateTime.Now;
            employeePIPS.ModifyTime = DateTime.Now;
            employeePIPS.Disable = false;
            employeePIPS.Status = status;
            employeePIPS.PIPS = tpHit;
            db.EmployeePIPs.Add(employeePIPS);
            db.SaveChanges();
        }
    }
}