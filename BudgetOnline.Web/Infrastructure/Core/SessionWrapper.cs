using System;
using System.Web;
using BudgetOnline.Contracts;

namespace BudgetOnline.Web.Infrastructure.Core
{
	public class SessionWrapper : ISessionWrapper
	{
		public bool IsAvailable { get { return HttpContext.Current.Session != null && HttpContext.Current.Session.Mode != System.Web.SessionState.SessionStateMode.Off; } }

		/// <summary>
		/// Put object to session
		/// </summary>
		/// <param name="key">Key of the session item</param>
		/// <param name="obj">Item for session</param>
		public void Put(string key, object obj)
		{
			if (HttpContext.Current.Session[key] != null)
				HttpContext.Current.Session.Remove(key);

			HttpContext.Current.Session.Add(key, obj);
		}

		/// <summary>
		/// Retrieve item from session
		/// </summary>
		/// <typeparam name="T">Type of item in session</typeparam>
		/// <param name="key">Name of item in session</param>
		/// <param name="value">Item from session. Default(T) if
		/// item doesn't exist.</param>
		/// <returns>Item from session</returns>
		public T Get<T>(string key)
		{
			try
			{
				if (!Exists(key))
					return default(T);

				return (T)HttpContext.Current.Session[key];
			}
			catch
			{
				return default(T);
			}
		}

		/// <summary>
		/// Retrieve item from session
		/// </summary>
		/// <typeparam name="T">Type of item in session</typeparam>
		/// <param name="key">Name of item in session</param>
		/// <param name="result"> </param>
		/// <returns>Item from session</returns>
		public bool Get<T>(string key, out T result)
		{
			try
			{
				if (Exists(key))
				{
					result = (T)HttpContext.Current.Session[key];
					return true;
				}
			}
			catch
			{
			}
	
			result = default(T);
			return false;
		}
		/// <summary>
		/// Check for item in session
		/// </summary>
		/// <param name="key">Name of item in session</param>
		/// <returns></returns>
		public bool Exists(string key)
		{
			return HttpContext.Current.Session[key] != null;
		}

		/// <summary>
		/// Remove object from session
		/// </summary>
		/// <param name="key">Key of the session item</param>
		public void Remove(string key)
		{
			if (Exists(key))
				HttpContext.Current.Session.Remove(key);
		}

		public void Abandon()
		{
			HttpContext.Current.Session.Abandon();
		}
	}

}