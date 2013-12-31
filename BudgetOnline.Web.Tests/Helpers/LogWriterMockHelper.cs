using BudgetOnline.Common.Contracts;
using Moq;

namespace BudgetOnline.Web.Tests.Helpers
{
    public class LogWriterMockHelper
    {
        public static Mock<ILogWriter> CreateMock()
        {
            var logWriter = new Mock<ILogWriter>();
            logWriter.SetupAllProperties();
            return logWriter;
        }
    }
}
