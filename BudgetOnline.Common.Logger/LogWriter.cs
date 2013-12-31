using System;
using BudgetOnline.Common.Contracts;
using NLog;

namespace BudgetOnline.Common.Logger
{
	public class LogWriter : ILogWriter
	{
		static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger(); 
 
		public static LogWriter GetInstance()
		{
			return new LogWriter();
		}

		public virtual void Trace(string message)
		{
			Logger.Trace(message);
		}

		public void TraceFormat(string message, params object[] parameters)
		{
			Trace(string.Format(message, parameters));
		}

		public virtual void Info(string message)
		{
			Logger.Info(message);
		}

		public void InfoFormat(string message, params object[] parameters)
		{
			Info(string.Format(message, parameters));
		}

		public virtual void Debug(string message)
		{
			Logger.Debug(message);
		}

		public void DebugFormat(string message, params object[] parameters)
		{
			Debug(string.Format(message, parameters));
		}

		public virtual void Warn(string message)
		{
			Logger.Warn(message);
		}

		public void WarnFormat(string message, params object[] parameters)
		{
			Warn(string.Format(message, parameters));
		}

		public virtual void Error(string message)
		{
			Logger.Error(message);
		}

		public virtual void Error(string message, params object[] args)
		{
			Logger.Error(message, args);
		}

		public virtual void Error(Exception error)
		{
			Logger.Error(error);
		}

		public virtual void Error(string message, Exception error)
		{
			Logger.Error(message);
			Logger.Error(error);
		}
	}
}
