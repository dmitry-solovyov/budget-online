using System;
using System.Collections.Generic;
using BudgetOnline.Contracts;

namespace BudgetOnline.Common.Contracts
{
	public interface ICachedStorage
	{
		Stack<CachedStorageItem> StorageSlots { get; }

		CachedStorageConfiguration Configuration { get; set; }

		void PutToCache<T>(T obj, string key, TimeSpan expireAfter) where T : class;
		T GetCahedObject<T>(string key) where T : class;
		T GetCahedObject<T>(string key, TimeSpan expireAfter, Func<T> objectInitiator) where T : class;
		void RemoveObject(string key);
		bool IsObjectInCache(string key);
	}
}
