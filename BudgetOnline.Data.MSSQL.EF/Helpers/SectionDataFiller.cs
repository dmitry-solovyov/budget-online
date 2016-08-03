using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.Helpers
{
    public static class SectionDataFiller
    {
        public static void FillData(BudgetDatabase dataContext, Guid? sectionId = null)
        {
            if (!sectionId.HasValue)
            {
                sectionId = new Guid("{77605774-CE29-4605-879B-A0A8CED1B49E}");
            }

            if (!dataContext.Sections.Any(x => x.Id == sectionId))
                dataContext.Sections.Add(
                  new SectionRecord
                  {
                      Id = sectionId.Value,
                      Description = "default",
                      CreatedWhen = DateTime.UtcNow,
                      CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                      IsDisabled = false
                  }
                );


            if (!dataContext.CategorySectionMaps.Any(x => x.CategoryId == new Guid("{972919F1-A7DE-4D7E-A7A1-389591DF9A93}") && x.SectionId == sectionId.Value))
                dataContext.CategorySectionMaps.Add(
                    new CategorySectionMapRecord
                    {
                        Id = Guid.NewGuid(),
                        SectionId = sectionId.Value,
                        CategoryId = new Guid("{972919F1-A7DE-4D7E-A7A1-389591DF9A93}"),
                        CreatedWhen = DateTime.UtcNow,
                        CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}")
                    }
                );


            FillAccounts(dataContext, sectionId.Value);
        }

        private static void FillAccounts(BudgetDatabase dataContext, Guid sectionId)
        {
            if (dataContext.Accounts.Any(x => x.SectionId == sectionId))
            {
                return;
            }

            var items = new Dictionary<string, int>
            {
                {"Наличные", 1},
                {"Кредитная карта", 2},
                {"Счет в банке", 3}
            };

            foreach (var item in items)
            {
                if (!dataContext.Accounts.Any(x => x.Name == item.Key && x.SectionId == sectionId))
                    dataContext.Accounts.AddOrUpdate(
                        x => x.Id,
                        new AccountRecord
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Key,
                            SortOrder = item.Value,
                            SectionId = sectionId,
                            IsDisabled = false,
                            Icon = null,
                            Description = null,
                            IsExternal = false,
                            CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                            CreatedWhen = DateTime.UtcNow
                        }
                    );
            }
        }
    }
}
