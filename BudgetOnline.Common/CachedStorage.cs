using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Contracts;

namespace BudgetOnline.Common
{
    public class CachedStorage : ICachedStorage
    {
        //private const string CachedStorageKey = "cachedStorageKey";
        private const string CachedStorageSlotsKey = "cachedStorageSlotsKey";
        private const string CachedStorageConfigurationKey = "cachedStorageConfigurationKey";

        public ISessionWrapper SessionWrapper { get; set; }

        public Stack<CachedStorageItem> StorageSlots
        {
            get
            {
                Stack<CachedStorageItem> storageSlots;
                if (!SessionWrapper.Get(CachedStorageSlotsKey, out storageSlots) || storageSlots == null)
                {
                    storageSlots = new Stack<CachedStorageItem>();
                    SessionWrapper.Put(CachedStorageSlotsKey, storageSlots);
                }

                return storageSlots;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentException("StorageSlots");

                SessionWrapper.Put(CachedStorageSlotsKey, value);
            }
        }

        public CachedStorageConfiguration Configuration
        {
            get
            {
                CachedStorageConfiguration configuration;
                if (!SessionWrapper.Get(CachedStorageConfigurationKey, out configuration) || configuration == null)
                {
                    configuration = new CachedStorageConfiguration();
                    SessionWrapper.Put(CachedStorageConfigurationKey, configuration);
                }

                return configuration;
            }
            set
            {
                if (value == null)
                    throw new ArgumentException("Configuration");

                SessionWrapper.Put(CachedStorageConfigurationKey, value);
            }
        }

        private CachedStorageItem FindInCache(string key, Stack<CachedStorageItem> storageSlots)
        {
            var slots = storageSlots;

            return slots.FirstOrDefault(item => item.Key == key && item.Data != null);
        }

        public void PutToCache<T>(T obj, string key)
            where T : class
        {
            PutToCache(obj, key, TimeSpan.MinValue);
        }

        public void PutToCache<T>(T obj, string key, TimeSpan expireAfter)
            where T : class
        {
            var configuration = Configuration;
            var slots = StorageSlots;

            var oldEntry = FindInCache(key, slots);
            if (oldEntry != null)
            {
                oldEntry.Data = obj;
            }
            else if (obj != null)
            {
                if (slots.Count + 1 > configuration.NumberOfSlots)
                {
                    //var oldCachedStorageItem = slots.Pop();
                }

                var cachedStorageItem = new CachedStorageItem { Key = key, Data = obj };
                slots.Push(cachedStorageItem);
            }

            StorageSlots = slots;
        }

        public void RemoveObject(string key)
        {
            if (IsObjectInCache(key))
            {
                var slots = StorageSlots;
                var oldEntry = FindInCache(key, slots);
                if (oldEntry != null)
                    oldEntry.Data = null;
            }
        }

        public T GetCahedObject<T>(string key)
            where T : class
        {
            return GetCahedObject<T>(key, TimeSpan.MinValue, null);
        }

        public T GetCahedObject<T>(string key, TimeSpan expireAfter, Func<T> objectInitiator)
            where T : class
        {
            var slots = StorageSlots;
            T result = default(T);

            if (IsObjectInCache(key))
                result = (T)FindInCache(key, slots).Data;
            else
                if (objectInitiator != null)
                {
                    result = objectInitiator();
                    PutToCache(result, key, expireAfter);
                }

            StorageSlots = slots;

            return result;
        }

        public bool IsObjectInCache(string key)
        {
            var slots = StorageSlots;
            var itemInCache = FindInCache(key, slots);

            return itemInCache != null && itemInCache.Data != null;
        }
    }



}