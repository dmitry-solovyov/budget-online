using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.MSSQL.EF.DataModels;
using BudgetOnline.Data.MSSQL.EF.Helpers;

namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new SqlDefaultValueAnnotationGenerator();
            SetSqlGenerator("System.Data.SqlClient", new NonClusteredPrimaryKeySqlMigrationSqlGenerator());
        }

        protected override void Seed(BudgetDatabase context)
        {
            PopulateUsers(context);
            context.SaveChanges();
            
            PopulateUserSessionStatuses(context);
            context.SaveChanges();

            PopulateSettings(context);
            context.SaveChanges();
            
            PopulateCategories(context);
            context.SaveChanges();
            
            PopulateOperationTypes(context);
            context.SaveChanges();
            
            PopulatePermissions(context);
            context.SaveChanges();
            
            PopulateSystemModules(context);
            context.SaveChanges();
            
            PopulateSystemModulePermissionMaps(context);
            context.SaveChanges();

            SectionDataFiller.FillData(context);
            context.SaveChanges();
        }

        private void PopulateUsers(BudgetDatabase context)
        {
            if (!context.Users.Any(x => x.Id == new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}")))
            {
                context.Users.Add(
                  new User
                  {
                      Id = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                      Email = "admin@budget.com",
                      UserName = "Domain Admin",
                      CreatedWhen = DateTime.UtcNow,
                      CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                      SectionId = null
                  }
                );

                context.UserPasswords.Add(
                    new UserPassword
                    {
                        Id = Guid.NewGuid(),
                        UserId = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                        CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                        CreatedWhen = DateTime.UtcNow,
                        Password = PasswordManager.HashPassword("123")
                    });
            }
        }

        private void PopulateSettings(BudgetDatabase context)
        {
            var items = new Dictionary<string, string>
            {
                {"Session.TokenValidityPeriod", "12:00:00"},
                {"Web.PageSize", "20"},
                {"Web.Transactions.PageSize", "50"}
            };

            foreach (var item in items)
            {
                if (!context.Settings.Any(x => x.Name == item.Key && x.Value == item.Value && x.SectionId == null))
                    context.Settings.AddOrUpdate(
                        x => x.Id,
                        new Setting
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Key,
                            Value = item.Value,
                            SectionId = null,
                            IsDisabled = false,
                            Description = null,
                            CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                            CreatedWhen = DateTime.UtcNow
                        }
                    );
            }
        }

        private void PopulateCategories(BudgetDatabase context)
        {
            if (!context.Categories.Any(x => x.Id == new Guid("{972919F1-A7DE-4D7E-A7A1-389591DF9A93}")))
                context.Categories.AddOrUpdate(
                    x => x.Id,
                    new Category
                    {
                        Id = new Guid("{972919F1-A7DE-4D7E-A7A1-389591DF9A93}"),
                        ParentId = null,
                        Name = "Питание",
                        IsDisabled = false
                    }
                );
        }

        private void PopulateOperationTypes(BudgetDatabase context)
        {
            var operationTypes = new Dictionary<int, string>
            {
                {(int)OperationTypes.Income, "Доход"},
                {(int)OperationTypes.Outcome, "Расход"},
                {(int)OperationTypes.Transfer, "Перевод"},
                {(int)OperationTypes.Exchange, "Обмен"},
                {(int)OperationTypes.Correction, "Корректировка"}
            };

            foreach (var operationType in operationTypes)
            {
                if (!context.OperationTypes.Any(x => x.Id == operationType.Key))
                    context.OperationTypes.AddOrUpdate(
                        x => x.Id,
                        new OperationType
                        {
                            Id = operationType.Key,
                            Name = operationType.Value
                        }
                    );
            }
        }

        private void PopulateUserSessionStatuses(BudgetDatabase context)
        {
            var items = new Dictionary<int, string>
            {
                {(int)UserSessionStatuses.Approved, "Удачный"},
                {(int)UserSessionStatuses.ApprovedFirstLogin, "Удачный в первый раз"},
                {(int)UserSessionStatuses.PasswordExpired, "Пароль устарел"},
                {(int)UserSessionStatuses.Rejected, "Неудачный"}
            };

            foreach (var item in items)
            {
                if (!context.UserSessionStatuses.Any(x => x.Id == item.Key))
                    context.UserSessionStatuses.AddOrUpdate(
                        x => x.Id,
                        new UserSessionStatus
                        {
                            Id = item.Key,
                            Name = item.Value
                        }
                    );
            }
        }

        private void PopulatePermissions(BudgetDatabase context)
        {
            var permissions = new Dictionary<int, Tuple<string, string>>
            {
                {(int)Permissions.AllowRead, new Tuple<string, string>("Чтение", "AllowRead")},
                {(int)Permissions.AllowCreate, new Tuple<string, string>("Создание", "AllowCreate")},
                {(int)Permissions.AllowUpdate, new Tuple<string, string>("Обновление", "AllowUpdate")},
                {(int)Permissions.AllowDelete, new Tuple<string, string>("Удаление", "AllowDelete")},
                {(int)Permissions.AllowDownload, new Tuple<string, string>("Скачивание", "AllowDownload")}
            };

            foreach (var permission in permissions)
            {
                if (!context.Permissions.Any(x => x.Id == permission.Key))
                    context.Permissions.AddOrUpdate(
                        x => x.Id,
                        new Permission
                        {
                            Id = permission.Key,
                            Code = permission.Value.Item2,
                            Description = permission.Value.Item1
                        }
                    );
            }
        }

        private void PopulateSystemModules(BudgetDatabase context)
        {
            var systemModules = new Dictionary<int, string>
            {
                {(int)SystemModules.TransactionHistory, "Транзакции"},
                {(int)SystemModules.TransactionPlanning, "Планирование"},
                {(int)SystemModules.Statistics, "Просмотр статистики"},
                {(int)SystemModules.Administration, "Администрирование"}
            };

            foreach (var systemModule in systemModules)
            {
                if (!context.SystemModules.Any(x => x.Id == systemModule.Key))
                    context.SystemModules.AddOrUpdate(
                        x => x.Id,
                        new SystemModule
                        {
                            Id = systemModule.Key,
                            Description = systemModule.Value
                        }
                    );
            }
        }

        private void PopulateSystemModulePermissionMaps(BudgetDatabase context)
        {
            var items = new[]
            {
                new {SystemModuleId = (int)SystemModules.TransactionHistory, PermissionId = (int)Permissions.AllowRead},
                new {SystemModuleId = (int)SystemModules.TransactionHistory, PermissionId = (int)Permissions.AllowCreate},
                new {SystemModuleId = (int)SystemModules.TransactionHistory, PermissionId = (int)Permissions.AllowUpdate},
                new {SystemModuleId = (int)SystemModules.TransactionHistory, PermissionId = (int)Permissions.AllowDelete},

                new {SystemModuleId = (int)SystemModules.Administration, PermissionId = (int)Permissions.AllowRead},
                new {SystemModuleId = (int)SystemModules.Administration, PermissionId = (int)Permissions.AllowCreate},
                new {SystemModuleId = (int)SystemModules.Administration, PermissionId = (int)Permissions.AllowUpdate},
                new {SystemModuleId = (int)SystemModules.Administration, PermissionId = (int)Permissions.AllowDelete},

                new {SystemModuleId = (int)SystemModules.Statistics, PermissionId = (int)Permissions.AllowRead}
            };

            foreach (var item in items)
            {
                if (!context.PermissionSystemModuleMaps.Any(x => x.PermissionId == item.PermissionId && x.SystemModuleId == item.SystemModuleId))
                    context.PermissionSystemModuleMaps.AddOrUpdate(
                        x => x.Id,
                        new PermissionSystemModuleMap
                        {
                            Id = Guid.NewGuid(),
                            PermissionId = item.PermissionId,
                            SystemModuleId = item.SystemModuleId,
                            CreatedWhen = DateTime.UtcNow,
                            CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}")
                        }
                    );
            }
        }
    }
}
