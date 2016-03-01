namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 1024),
                        Icon = c.String(maxLength: 512),
                        SortOrder = c.Int(nullable: false),
                        IsExternal = c.Boolean(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SectionId, name: "IX_Account_SectionId")
                .Index(t => new { t.SectionId, t.Name }, unique: true, clustered: true, name: "UX_Account_AccountSectionName")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(),
                        Email = c.String(nullable: false, maxLength: 255),
                        UserName = c.String(nullable: false, maxLength: 255),
                        ValidFrom = c.DateTime(),
                        ValidTo = c.DateTime(),
                        AccessQuestion = c.String(maxLength: 1024),
                        AccessQuestionAnswer = c.String(maxLength: 1024),
                        IsLocked = c.Boolean(nullable: false),
                        LockStarted = c.DateTime(),
                        AccessFailedCount = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId, name: "IX_User_SectionId")
                .Index(t => t.Email, unique: true, name: "IX_UserEmail")
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 255),
                        IsDisabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.AdminUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 1024),
                        Icon = c.String(maxLength: 512),
                        IsDisabled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId, name: "IX_Category_ParentId")
                .Index(t => t.Name, unique: true, name: "UX_Category_Name");
            
            CreateTable(
                "dbo.CategorySectionMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => new { t.SectionId, t.CategoryId }, name: "IX_CategorySectionMap_GroupKey")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Symbol = c.String(maxLength: 5),
                        Description = c.String(maxLength: 1024),
                        Icon = c.String(maxLength: 512),
                        IsDisabled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UX_Currency_Name");
            
            CreateTable(
                "dbo.CurrencySectionMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => new { t.SectionId, t.CurrencyId }, name: "IX_CurrencySectionMap_GroupKey")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.OperationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OperationTypeSectionMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        OperationTypeId = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.OperationTypes", t => t.OperationTypeId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => new { t.SectionId, t.OperationTypeId }, name: "IX_OperationTypeSectionMap_GroupKey")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 1024),
                        IsDisabled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true, name: "UX_Permission_Code");
            
            CreateTable(
                "dbo.PermissionSystemModuleMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        SystemModuleId = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.SystemModules", t => t.SystemModuleId)
                .Index(t => new { t.PermissionId, t.SystemModuleId }, clustered: true, name: "IX_PermissionSystemModuleMap_GroupKey")
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.SystemModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 1024),
                        IsDisabled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionSystemModuleUserMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        SystemModuleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.SystemModules", t => t.SystemModuleId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.PermissionId, t.SystemModuleId, t.UserId }, name: "IX_PermissionSystemModuleUserMap_GroupKey")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 255),
                        Value = c.String(nullable: false),
                        Description = c.String(maxLength: 1024),
                        IsDisabled = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SectionId, name: "IX_Setting_SectionId")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Icon = c.String(maxLength: 512),
                        IsDisabled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .Index(t => t.Name, unique: true, name: "UX_Tag_Name")
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.TransactionCorrectionDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        TransactionDetailId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        TotalSum = c.Double(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Transactions", t => t.TransactionId)
                .ForeignKey("dbo.TransactionDetails", t => t.TransactionDetailId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SectionId, name: "IX_TransactionCorrectionDetail_SectionId")
                .Index(t => t.TransactionId, clustered: true, name: "IX_TransactionCorrectionDetail_TransactionId")
                .Index(t => t.TransactionDetailId, name: "IX_TransactionCorrectionDetail_TransactionDetailId")
                .Index(t => t.AccountId)
                .Index(t => t.CurrencyId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        OperationTypeId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Formula = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.OperationTypes", t => t.OperationTypeId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SectionId, name: "IX_Transaction_SectionId")
                .Index(t => t.CategoryId, name: "IX_Transaction_CategoryId")
                .Index(t => t.OperationTypeId, name: "IX_Transaction_OperationTypeId")
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        Sum = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Transactions", t => t.TransactionId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SectionId, name: "IX_TransactionDetail_SectionId")
                .Index(t => t.TransactionId, clustered: true, name: "IX_TransactionDetail_TransactionId")
                .Index(t => t.AccountId)
                .Index(t => t.CurrencyId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.TransactionTagMaps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .ForeignKey("dbo.Transactions", t => t.TransactionId)
                .Index(t => new { t.TransactionId, t.TagId }, clustered: true, name: "IX_TransactionTagMap_GroupKey")
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.UserPasswords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Password = c.String(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId, name: "IX_UserPassword_UserId")
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UserPasswordId = c.Guid(nullable: false),
                        UserSessionStatusId = c.Int(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.UserPasswords", t => t.UserPasswordId)
                .ForeignKey("dbo.UserSessionStatus", t => t.UserSessionStatusId)
                .Index(t => new { t.UserId, t.CreatedWhen }, clustered: true, name: "IX_UserSession_GroupKey")
                .Index(t => t.UserPasswordId)
                .Index(t => t.UserSessionStatusId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.UserSessionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSessions", "UserSessionStatusId", "dbo.UserSessionStatus");
            DropForeignKey("dbo.UserSessions", "UserPasswordId", "dbo.UserPasswords");
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSessions", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.UserPasswords", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserPasswords", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionTagMaps", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.TransactionTagMaps", "TagId", "dbo.Tags");
            DropForeignKey("dbo.TransactionTagMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionCorrectionDetails", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionCorrectionDetails", "TransactionDetailId", "dbo.TransactionDetails");
            DropForeignKey("dbo.TransactionDetails", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionDetails", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.TransactionDetails", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.TransactionDetails", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.TransactionDetails", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionDetails", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.TransactionCorrectionDetails", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Transactions", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Transactions", "OperationTypeId", "dbo.OperationTypes");
            DropForeignKey("dbo.Transactions", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TransactionCorrectionDetails", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.TransactionCorrectionDetails", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.TransactionCorrectionDetails", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.TransactionCorrectionDetails", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Tags", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Settings", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Settings", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Settings", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.PermissionSystemModuleUserMaps", "UserId", "dbo.Users");
            DropForeignKey("dbo.PermissionSystemModuleUserMaps", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PermissionSystemModuleUserMaps", "SystemModuleId", "dbo.SystemModules");
            DropForeignKey("dbo.PermissionSystemModuleUserMaps", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.PermissionSystemModuleUserMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.PermissionSystemModuleMaps", "SystemModuleId", "dbo.SystemModules");
            DropForeignKey("dbo.PermissionSystemModuleMaps", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.PermissionSystemModuleMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.OperationTypeSectionMaps", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.OperationTypeSectionMaps", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.OperationTypeSectionMaps", "OperationTypeId", "dbo.OperationTypes");
            DropForeignKey("dbo.OperationTypeSectionMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.CurrencySectionMaps", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.CurrencySectionMaps", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CurrencySectionMaps", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CurrencySectionMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.CategorySectionMaps", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.CategorySectionMaps", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CategorySectionMaps", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.CategorySectionMaps", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.AdminUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.AdminUsers", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Accounts", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Accounts", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Accounts", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Sections", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedBy", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "CreatedBy" });
            DropIndex("dbo.UserSessions", new[] { "UserSessionStatusId" });
            DropIndex("dbo.UserSessions", new[] { "UserPasswordId" });
            DropIndex("dbo.UserSessions", "IX_UserSession_GroupKey");
            DropIndex("dbo.UserPasswords", new[] { "CreatedBy" });
            DropIndex("dbo.UserPasswords", "IX_UserPassword_UserId");
            DropIndex("dbo.TransactionTagMaps", new[] { "CreatedBy" });
            DropIndex("dbo.TransactionTagMaps", "IX_TransactionTagMap_GroupKey");
            DropIndex("dbo.TransactionDetails", new[] { "UpdatedBy" });
            DropIndex("dbo.TransactionDetails", new[] { "CreatedBy" });
            DropIndex("dbo.TransactionDetails", new[] { "CurrencyId" });
            DropIndex("dbo.TransactionDetails", new[] { "AccountId" });
            DropIndex("dbo.TransactionDetails", "IX_TransactionDetail_TransactionId");
            DropIndex("dbo.TransactionDetails", "IX_TransactionDetail_SectionId");
            DropIndex("dbo.Transactions", new[] { "UpdatedBy" });
            DropIndex("dbo.Transactions", new[] { "CreatedBy" });
            DropIndex("dbo.Transactions", "IX_Transaction_OperationTypeId");
            DropIndex("dbo.Transactions", "IX_Transaction_CategoryId");
            DropIndex("dbo.Transactions", "IX_Transaction_SectionId");
            DropIndex("dbo.TransactionCorrectionDetails", new[] { "UpdatedBy" });
            DropIndex("dbo.TransactionCorrectionDetails", new[] { "CreatedBy" });
            DropIndex("dbo.TransactionCorrectionDetails", new[] { "CurrencyId" });
            DropIndex("dbo.TransactionCorrectionDetails", new[] { "AccountId" });
            DropIndex("dbo.TransactionCorrectionDetails", "IX_TransactionCorrectionDetail_TransactionDetailId");
            DropIndex("dbo.TransactionCorrectionDetails", "IX_TransactionCorrectionDetail_TransactionId");
            DropIndex("dbo.TransactionCorrectionDetails", "IX_TransactionCorrectionDetail_SectionId");
            DropIndex("dbo.Tags", new[] { "CreatedBy" });
            DropIndex("dbo.Tags", "UX_Tag_Name");
            DropIndex("dbo.Settings", new[] { "UpdatedBy" });
            DropIndex("dbo.Settings", new[] { "CreatedBy" });
            DropIndex("dbo.Settings", "IX_Setting_SectionId");
            DropIndex("dbo.PermissionSystemModuleUserMaps", new[] { "UpdatedBy" });
            DropIndex("dbo.PermissionSystemModuleUserMaps", new[] { "CreatedBy" });
            DropIndex("dbo.PermissionSystemModuleUserMaps", "IX_PermissionSystemModuleUserMap_GroupKey");
            DropIndex("dbo.PermissionSystemModuleMaps", new[] { "CreatedBy" });
            DropIndex("dbo.PermissionSystemModuleMaps", "IX_PermissionSystemModuleMap_GroupKey");
            DropIndex("dbo.Permissions", "UX_Permission_Code");
            DropIndex("dbo.OperationTypeSectionMaps", new[] { "UpdatedBy" });
            DropIndex("dbo.OperationTypeSectionMaps", new[] { "CreatedBy" });
            DropIndex("dbo.OperationTypeSectionMaps", "IX_OperationTypeSectionMap_GroupKey");
            DropIndex("dbo.CurrencySectionMaps", new[] { "UpdatedBy" });
            DropIndex("dbo.CurrencySectionMaps", new[] { "CreatedBy" });
            DropIndex("dbo.CurrencySectionMaps", "IX_CurrencySectionMap_GroupKey");
            DropIndex("dbo.Currencies", "UX_Currency_Name");
            DropIndex("dbo.CategorySectionMaps", new[] { "UpdatedBy" });
            DropIndex("dbo.CategorySectionMaps", new[] { "CreatedBy" });
            DropIndex("dbo.CategorySectionMaps", "IX_CategorySectionMap_GroupKey");
            DropIndex("dbo.Categories", "UX_Category_Name");
            DropIndex("dbo.Categories", "IX_Category_ParentId");
            DropIndex("dbo.AdminUsers", new[] { "CreatedBy" });
            DropIndex("dbo.AdminUsers", new[] { "UserId" });
            DropIndex("dbo.Sections", new[] { "UpdatedBy" });
            DropIndex("dbo.Sections", new[] { "CreatedBy" });
            DropIndex("dbo.Users", new[] { "CreatedBy" });
            DropIndex("dbo.Users", "IX_UserEmail");
            DropIndex("dbo.Users", "IX_User_SectionId");
            DropIndex("dbo.Accounts", new[] { "UpdatedBy" });
            DropIndex("dbo.Accounts", new[] { "CreatedBy" });
            DropIndex("dbo.Accounts", "UX_Account_AccountSectionName");
            DropIndex("dbo.Accounts", "IX_Account_SectionId");
            DropTable("dbo.UserSessionStatus");
            DropTable("dbo.UserSessions");
            DropTable("dbo.UserPasswords");
            DropTable("dbo.TransactionTagMaps");
            DropTable("dbo.TransactionDetails");
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionCorrectionDetails");
            DropTable("dbo.Tags");
            DropTable("dbo.Settings");
            DropTable("dbo.PermissionSystemModuleUserMaps");
            DropTable("dbo.SystemModules");
            DropTable("dbo.PermissionSystemModuleMaps");
            DropTable("dbo.Permissions");
            DropTable("dbo.OperationTypeSectionMaps");
            DropTable("dbo.OperationTypes");
            DropTable("dbo.CurrencySectionMaps");
            DropTable("dbo.Currencies");
            DropTable("dbo.CategorySectionMaps");
            DropTable("dbo.Categories");
            DropTable("dbo.AdminUsers");
            DropTable("dbo.Sections");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
