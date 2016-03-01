using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    public class NonClusteredPrimaryKeySqlMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation)
        {
            addPrimaryKeyOperation.IsClustered = false;
            base.Generate(addPrimaryKeyOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            createTableOperation.PrimaryKey.IsClustered = false;
            base.Generate(createTableOperation);
        }

        protected override void Generate(MoveTableOperation moveTableOperation)
        {
            moveTableOperation.CreateTableOperation.PrimaryKey.IsClustered = false;
            base.Generate(moveTableOperation);
        }

        //public override IEnumerable<System.Data.Entity.Migrations.Sql.MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
        //{
        //    var operations = migrationOperations as IList<MigrationOperation> ?? migrationOperations.ToList();

        //    var primaries = operations.OfType<CreateTableOperation>().Where(x => x.PrimaryKey.IsClustered).Select(x => x.PrimaryKey).ToList();
        //    var indexes = operations.OfType<CreateIndexOperation>().Where(x => x.IsClustered).ToList();

        //    foreach (var index in indexes)
        //    {
        //        var primary = primaries.SingleOrDefault(x => x.Table == index.Table);
        //        if (primary != null)
        //        {
        //            primary.IsClustered = false;
        //        }
        //    }
        //    return base.Generate(operations, providerManifestToken);
        //}
    }
}
