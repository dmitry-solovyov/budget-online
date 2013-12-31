using System;

namespace BudgetOnline.Common.Contracts
{
	public interface ICacheWrapper
	{
		bool IsAvailable { get; }
		void Put(string key, object obj, TimeSpan delay);
		void Put(string key, object obj, DateTime expireDate);
		T Get<T>(string key);
		bool Exists(string key);
		void Remove(string key);
		void Abandon();

		TimeSpan GetDefaultSettingCacheTimeout { get; }
	}
}
