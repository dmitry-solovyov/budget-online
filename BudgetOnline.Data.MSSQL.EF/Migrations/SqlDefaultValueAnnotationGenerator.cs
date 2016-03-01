using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    public class SqlDefaultValueAnnotationGenerator : MigrationCodeGenerator
    {
        public override ScaffoldedMigration Generate(
            string migrationId,
            IEnumerable<MigrationOperation> operations, 
            string sourceModel, 
            string targetModel, 
            string @namespace, 
            string className)
        {
            var migrationOperations = operations as MigrationOperation[] ?? operations.ToArray();

            foreach (var operation in migrationOperations)
            {
                if (operation is CreateTableOperation)
                {
                    foreach (var column in ((CreateTableOperation)operation).Columns)
                        if (column.ClrType == typeof(DateTime) && column.Annotations.Any())
                        {
                            AnnotationValues values;
                            if (column.Annotations.TryGetValue("SqlDefaultValue", out values))
                            {
                                column.Annotations.Remove("SqlDefaultValue");
                                column.DefaultValueSql = values.NewValue.ToString();
                            }
                        }
                }
                else if (operation is AddColumnOperation)
                {
                    var column = ((AddColumnOperation)operation).Column;
                    if (column.ClrType == typeof(DateTime) && column.Annotations.Any())
                    {
                        AnnotationValues values;
                        if (column.Annotations.TryGetValue("SqlDefaultValue", out values))
                        {
                            column.Annotations.Remove("SqlDefaultValue");
                            column.DefaultValueSql = values.NewValue.ToString();
                        }

                    }
                }
            }

            var generator = new CSharpMigrationCodeGenerator();

            return generator.Generate(migrationId, migrationOperations, sourceModel, targetModel, @namespace, className);
        }
    }
}
