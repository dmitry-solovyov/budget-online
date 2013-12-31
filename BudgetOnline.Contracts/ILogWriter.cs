using System;
namespace BudgetOnline.Contracts
{
	public interface ILogWriter
	{
		void Trace(string message);
		void TraceFormat(string message, params object[] parameters);
		void Info(string message);
		void InfoFormat(string message, params object[] parameters);
		void Debug(string message);
		void DebugFormat(string message, params object[] parameters);
		void Warn(string message);
		void WarnFormat(string message, params object[] parameters);

		void Error(string message);
		void Error(Exception error);
		void Error(string message, Exception error);
	}
}
