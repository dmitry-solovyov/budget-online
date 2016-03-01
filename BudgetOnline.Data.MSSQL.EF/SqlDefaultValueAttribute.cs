using System;

namespace BudgetOnline.Data.MSSQL.EF
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlDefaultValueAttribute : Attribute
    {
        public SqlDefaultValueAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public string DefaultValue { get; private set; }
    }
}
