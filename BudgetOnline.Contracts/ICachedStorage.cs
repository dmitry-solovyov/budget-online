using System;
using System.Collections.Generic;
using BudgetOnline.Common;

namespace BudgetOnline.Contracts
{
	public interface ICachedStorage
	{
		Stack<CachedStorageItem> StorageSlots { get; }

		CachedStorageConfiguration Configuration { get; set; }

		void PutToCache<T>(T obj, string key, TimeSpan expireAfter);
		T GetCahedObject<T>(string key);
		T GetCahedObject<T>(string key, TimeSpan expireAfter, Func<T> objectInitiator);
		bool IsObjectInCache<T>(string key);
	}
}
