namespace Risen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllowNotifications",
                c => new
                    {
                        AllowNotificationId = c.Int(nullable: false, identity: true),
                        DeviceUserId = c.Int(nullable: false),
                        CompanyEmployeeID = c.Int(nullable: false),
                        RegisterComapanyID = c.Int(nullable: false),
                        status = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AllowNotificationId)
                .ForeignKey("dbo.CompanyEmployees", t => t.CompanyEmployeeID, cascadeDelete: false)
                .ForeignKey("dbo.RegisterComapanies", t => t.RegisterComapanyID, cascadeDelete: false)
                .Index(t => t.CompanyEmployeeID)
                .Index(t => t.RegisterComapanyID);
            
            CreateTable(
                "dbo.CompanyEmployees",
                c => new
                    {
                        CompanyEmployeeID = c.Int(nullable: false, identity: true),
                        fName = c.String(),
                        lName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Image = c.String(),
                        Salary = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        Companyid = c.Int(nullable: false),
                        Address = c.String(),
                        Contact = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModyfiedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModyfiedBy = c.String(),
                        Enable = c.Boolean(nullable: false),
                        DateOfBirth = c.String(),
                        DesignationId = c.Int(nullable: false),
                        CreatedById = c.Int(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyEmployeeID)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: false)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalLeavesDays = c.Int(nullable: false),
                        companyid = c.String(),
                        Enable = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.DesignationID);
            
            CreateTable(
                "dbo.RegisterComapanies",
                c => new
                    {
                        RegisterComapanyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Password = c.String(),
                        logo = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegisterComapanyID);
            
            CreateTable(
                "dbo.ConfirmationCodes",
                c => new
                    {
                        ConfirmationCodeID = c.Int(nullable: false, identity: true),
                        employeeid = c.String(),
                        ParentId = c.String(),
                        CustomerId = c.String(),
                        companyid = c.String(),
                        code = c.String(),
                        uniquekey = c.String(),
                    })
                .PrimaryKey(t => t.ConfirmationCodeID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrenciesId = c.Int(nullable: false, identity: true),
                        CurrencyListId = c.Int(nullable: false),
                        FirstCurrency = c.String(),
                        SecondCurrency = c.String(),
                        FirstCurrencyImage = c.String(),
                        SecondCurrencyImage = c.String(),
                        EmployeeId = c.Int(),
                        CreatedById = c.Int(),
                        Companyid = c.String(),
                        Disable = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CurrenciesId)
                .ForeignKey("dbo.CurrencyLists", t => t.CurrencyListId, cascadeDelete: false)
                .Index(t => t.CurrencyListId);
            
            CreateTable(
                "dbo.CurrencyLists",
                c => new
                    {
                        CurrencyListId = c.Int(nullable: false, identity: true),
                        CurrencyName = c.String(),
                        CurrencyNo = c.Int(nullable: false),
                        firstImage = c.String(),
                        SecondImage = c.String(),
                    })
                .PrimaryKey(t => t.CurrencyListId);
            
            CreateTable(
                "dbo.EmployeePIPS",
                c => new
                    {
                        EmployeePIPSId = c.Int(nullable: false, identity: true),
                        PIPS = c.Int(nullable: false),
                        TradingSignalId = c.Int(nullable: false),
                        CompanyEmployeeID = c.Int(nullable: false),
                        CompanyId = c.String(),
                        Disable = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeePIPSId)
                .ForeignKey("dbo.CompanyEmployees", t => t.CompanyEmployeeID, cascadeDelete: false)
                .ForeignKey("dbo.TradingSignals", t => t.TradingSignalId, cascadeDelete: false)
                .Index(t => t.TradingSignalId)
                .Index(t => t.CompanyEmployeeID);
            
            CreateTable(
                "dbo.TradingSignals",
                c => new
                    {
                        TradingSignalId = c.Int(nullable: false, identity: true),
                        CurrencyListId = c.Int(nullable: false),
                        Buy = c.String(),
                        Type = c.String(),
                        Remarks = c.String(),
                        Image = c.String(),
                        CurrentPrice = c.String(),
                        PIPS = c.Long(nullable: false),
                        FromImage = c.String(),
                        ToImage = c.String(),
                        Status = c.String(),
                        EmployeeId = c.Int(),
                        CreatedById = c.Int(),
                        Companyid = c.String(),
                        Disable = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TradingSignalId)
                .ForeignKey("dbo.CurrencyLists", t => t.CurrencyListId, cascadeDelete: false)
                .Index(t => t.CurrencyListId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        RegisterComapanyID = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        CurrencyName = c.String(),
                        EmployeeName = c.String(),
                        Status = c.String(),
                        Title = c.String(),
                        Body = c.String(),
                        Type = c.String(),
                        Price = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        TradingSignalId = c.String(),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.RegisterComapanies", t => t.RegisterComapanyID, cascadeDelete: false)
                .Index(t => t.RegisterComapanyID);
            
            CreateTable(
                "dbo.Privileges",
                c => new
                    {
                        PrivilegesId = c.Int(nullable: false, identity: true),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        MoyfiedBy = c.String(),
                        DesignationId = c.Int(nullable: false),
                        CompanyId = c.String(),
                        Enalbe = c.Boolean(nullable: false),
                        isClients = c.Boolean(nullable: false),
                        isClientView = c.Boolean(nullable: false),
                        isClientCreate = c.Boolean(nullable: false),
                        isClientUpdate = c.Boolean(nullable: false),
                        isClientDelet = c.Boolean(nullable: false),
                        isLeadUser = c.Boolean(nullable: false),
                        isLeadUserView = c.Boolean(nullable: false),
                        isLeadUserUpdate = c.Boolean(nullable: false),
                        isLeadUserConvertToCustomer = c.Boolean(nullable: false),
                        isCompany = c.Boolean(nullable: false),
                        isCompanyView = c.Boolean(nullable: false),
                        isCompanyCreate = c.Boolean(nullable: false),
                        isCompanyUpdate = c.Boolean(nullable: false),
                        isCompanyDelet = c.Boolean(nullable: false),
                        isDesignation = c.Boolean(nullable: false),
                        isDesignationView = c.Boolean(nullable: false),
                        isDesignationUpdate = c.Boolean(nullable: false),
                        isDesignationCreate = c.Boolean(nullable: false),
                        isProduct = c.Boolean(nullable: false),
                        isProductCreate = c.Boolean(nullable: false),
                        isProductView = c.Boolean(nullable: false),
                        isProductUpdate = c.Boolean(nullable: false),
                        isPolicy = c.Boolean(nullable: false),
                        isPolicyView = c.Boolean(nullable: false),
                        isPolicyCreate = c.Boolean(nullable: false),
                        isPolicyUpdate = c.Boolean(nullable: false),
                        isStaff = c.Boolean(nullable: false),
                        isStaffView = c.Boolean(nullable: false),
                        isStaffCreate = c.Boolean(nullable: false),
                        isStaffUpdate = c.Boolean(nullable: false),
                        isStaffDelet = c.Boolean(nullable: false),
                        isLeadStaff = c.Boolean(nullable: false),
                        isConverLeadPartner = c.Boolean(nullable: false),
                        IsDashboard = c.Boolean(nullable: false),
                        MyCommission = c.Boolean(nullable: false),
                        ViewPartnerCommisson = c.Boolean(nullable: false),
                        IsReport = c.Boolean(nullable: false),
                        IsSetting = c.Boolean(nullable: false),
                        isEmployee = c.Boolean(nullable: false),
                        isEmployeeView = c.Boolean(nullable: false),
                        isEmployeeCreate = c.Boolean(nullable: false),
                        isEmployeeUpdate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PrivilegesId)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: false)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.StopLoses",
                c => new
                    {
                        StopLoseId = c.Int(nullable: false, identity: true),
                        TradingSignalId = c.Int(nullable: false),
                        SL = c.String(),
                        PIPS = c.String(),
                        CreatedById = c.Int(),
                        Companyid = c.String(),
                        Disable = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StopLoseId);
            
            CreateTable(
                "dbo.TakeProfits",
                c => new
                    {
                        TakeProfitId = c.Int(nullable: false, identity: true),
                        TradingSignalId = c.Int(nullable: false),
                        TP = c.String(),
                        PIPS = c.String(),
                        CreatedById = c.Int(),
                        Companyid = c.String(),
                        Disable = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TakeProfitId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        DeviceUserId = c.Int(nullable: false),
                        ApiToken = c.String(),
                        DeviceToken = c.String(),
                        Disable = c.Boolean(nullable: false),
                        RegisterComapanyID = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.RegisterComapanies", t => t.RegisterComapanyID, cascadeDelete: false)
                .Index(t => t.RegisterComapanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RegisterComapanyID", "dbo.RegisterComapanies");
            DropForeignKey("dbo.Privileges", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Notifications", "RegisterComapanyID", "dbo.RegisterComapanies");
            DropForeignKey("dbo.EmployeePIPS", "TradingSignalId", "dbo.TradingSignals");
            DropForeignKey("dbo.TradingSignals", "CurrencyListId", "dbo.CurrencyLists");
            DropForeignKey("dbo.EmployeePIPS", "CompanyEmployeeID", "dbo.CompanyEmployees");
            DropForeignKey("dbo.Currencies", "CurrencyListId", "dbo.CurrencyLists");
            DropForeignKey("dbo.AllowNotifications", "RegisterComapanyID", "dbo.RegisterComapanies");
            DropForeignKey("dbo.AllowNotifications", "CompanyEmployeeID", "dbo.CompanyEmployees");
            DropForeignKey("dbo.CompanyEmployees", "DesignationId", "dbo.Designations");
            DropIndex("dbo.Users", new[] { "RegisterComapanyID" });
            DropIndex("dbo.Privileges", new[] { "DesignationId" });
            DropIndex("dbo.Notifications", new[] { "RegisterComapanyID" });
            DropIndex("dbo.TradingSignals", new[] { "CurrencyListId" });
            DropIndex("dbo.EmployeePIPS", new[] { "CompanyEmployeeID" });
            DropIndex("dbo.EmployeePIPS", new[] { "TradingSignalId" });
            DropIndex("dbo.Currencies", new[] { "CurrencyListId" });
            DropIndex("dbo.CompanyEmployees", new[] { "DesignationId" });
            DropIndex("dbo.AllowNotifications", new[] { "RegisterComapanyID" });
            DropIndex("dbo.AllowNotifications", new[] { "CompanyEmployeeID" });
            DropTable("dbo.Users");
            DropTable("dbo.TakeProfits");
            DropTable("dbo.StopLoses");
            DropTable("dbo.Privileges");
            DropTable("dbo.Notifications");
            DropTable("dbo.TradingSignals");
            DropTable("dbo.EmployeePIPS");
            DropTable("dbo.CurrencyLists");
            DropTable("dbo.Currencies");
            DropTable("dbo.ConfirmationCodes");
            DropTable("dbo.RegisterComapanies");
            DropTable("dbo.Designations");
            DropTable("dbo.CompanyEmployees");
            DropTable("dbo.AllowNotifications");
        }
    }
}
