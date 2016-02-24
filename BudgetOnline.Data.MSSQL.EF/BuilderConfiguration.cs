using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BudgetOnline.Common.Logger;
using BudgetOnline.Data.MSSQL.EF.DataModelBuilders.Base;

namespace BudgetOnline.Data.MSSQL.EF
{
    internal static class BuilderConfiguration
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            var logWriter = new LogWriter();
            logWriter.Debug("Building database model...");

            var builders = FindBuilders(modelBuilder);
            foreach (var builder in builders)
            {
                logWriter.DebugFormat("  Building model: {0}", builder.GetType().FullName);
                builder.Build();
            }

            logWriter.Debug("Building database model completed!");
        }

        private static IEnumerable<IModelBuilder> FindBuilders(DbModelBuilder modelBuilder)
        {
            return typeof(IModelBuilder)
                .Assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Any(t => t == typeof(IModelBuilder)))
                .Select(x => Activator.CreateInstance(x, modelBuilder))
                .OfType<IModelBuilder>();
        }
    }
}
