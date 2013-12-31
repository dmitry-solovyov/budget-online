using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Core
{
	public class CacheWrapper : ICacheWrapper
	{
		public ILogWriter Log { get; set; }

		private string SessionKey
		{
			get
			{
				if (HttpContext.Current.Session == null)
					return null;

				return HttpContext.Current.Session.SessionID;
			}
		}

		private string GetRealKey(string key)
		{
			return string.Format("{0}_{1}", SessionKey, key);
		}

		public bool IsAvailable
		{
			get
			{
				return HttpContext.Current.Cache != null
					&& HttpContext.Current.Session != null
					&& HttpContext.Current.Session.Mode != System.Web.SessionState.SessionStateMode.Off;
			}
		}

		public void Put(string key, object obj, TimeSpan delay)
		{
			string realKey = GetRealKey(key);

			if (HttpContext.Current.Cache[realKey] != null)
				HttpContext.Current.Cache.Remove(realKey);

			HttpContext.Current.Cache.Add(realKey, obj, null, Cache.NoAbsoluteExpiration, delay, CacheItemPriority.Default, null);
		}

		public void Put(string key, object obj, DateTime expireDate)
		{
			string realKey = GetRealKey(key);

			if (HttpContext.Current.Cache[realKey] != null)
				HttpContext.Current.Cache.Remove(realKey);

			HttpContext.Current.Cache.Add(realKey, obj, null, expireDate, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
		}

		public T Get<T>(string key)
		{
			string realKey = GetRealKey(key);

			try
			{
				if (!Exists(key))
					return default(T);

				return (T)HttpContext.Current.Cache[realKey];
			}
			catch
			{
				return default(T);
			}
		}

		public bool Exists(string key)
		{
			string realKey = GetRealKey(key);
			return HttpContext.Current.Cache[realKey] != null;
		}

		/// <summary>
		/// Remove object from session
		/// </summary>
		/// <param name="key">Key of the session item</param>
		public void Remove(string key)
		{
			if (Exists(key))
			{
				string realKey = GetRealKey(key);
				HttpContext.Current.Cache.Remove(realKey);
			}
		}

		public void Abandon()
		{
			var keys = new List<string>();

			var enumerator = HttpContext.Current.Cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Key.ToString().StartsWith(SessionKey))
				{
					keys.Add(enumerator.Key.ToString());
					Log.TraceFormat("Cache abandoned. Removing entry {0}", enumerator.Key);
				}
			}

			foreach (var key in keys)
			{
				HttpContext.Current.Cache.Remove(key);
			}
		}

		public TimeSpan GetDefaultSettingCacheTimeout
		{
			get { return new TimeSpan(0, 0, 10, 0); }
		}
	}

}