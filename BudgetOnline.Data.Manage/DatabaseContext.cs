using System.Configuration;
using System.Data.SqlClient;
using BudgetOnline.Data.MSSQL;

namespace BudgetOnline.Data.Manage
{
    public static class DatabaseContext
    {
        private const string ConnectionStringSettingName = "BudgetOnline.Data.MSSQL.Properties.Settings.Budget4ConnectionString";

        public static BudgetOnlineDBDataContext Get()
        {
            if (ConfigurationManager.ConnectionStrings[ConnectionStringSettingName] == null ||
                string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[ConnectionStringSettingName].ConnectionString))
                throw new ConfigurationErrorsException("Could not find configuration of SQL connection");

            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionStringSettingName].ConnectionString);

            var context = new BudgetOnlineDBDataContext(connection)
                              {
                                  ObjectTrackingEnabled = true,
                                  DeferredLoadingEnabled = false
                              };

            return context;
        }
    }
}
