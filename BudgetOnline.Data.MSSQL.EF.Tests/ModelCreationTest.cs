using NUnit.Framework;

namespace BudgetOnline.Data.MSSQL.EF.Tests
{
    [TestFixture]
    public class ModelCreationTest
    {
        [Test]
        public void Test1()
        {
            var model = new BudgetDatabase();
        }
    }
}
