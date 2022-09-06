using Risen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Risen.Contaxt
{
    public class RisenContext : DbContext
    {
        public RisenContext() : base("ConnectionStr")
        {
        }
     
        public DbSet<RegisterComapany> RegisterComapany { get; set; }
        public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<TakeProfit> TakeProfits { get; set; }
        public DbSet<StopLose> StopLoses { get; set; }
        public DbSet<TradingSignals> TradingSignals { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<Privileges> privileges { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeePIPS> EmployeePIPs { get; set; }
        public DbSet<CurrencyList> CurrencyLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AllowNotification> AllowNotifications { get; set; }
    }
}