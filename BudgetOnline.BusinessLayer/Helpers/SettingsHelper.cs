using System;
using BudgetOnline.Common;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.BusinessLayer.Helpers
{
    public class SettingsHelper : ISettingsHelper
    {
        public ISettingRepository SettingRepository { get; set; }
        public ICacheWrapper CacheWrapper { get; set; }

        public TimeSpan PasswordValidityPeriod(int sectionId)
        {
            var item = SettingRepository.FindByName(sectionId, null, "User.PasswordValidityPeriod");
            if (item != null)
                return item.Value.TryToTimeSpan(TimeSpan.Zero);

            return TimeSpan.Zero;
        }

        public TimeSpan TokenValidityPeriod(int sectionId)
        {
            var item = SettingRepository.FindByName(sectionId, null, "User.TokenValidityPeriod");
            if (item != null)
                return item.Value.TryToTimeSpan(TimeSpan.Zero);

            return TimeSpan.Zero;
        }

        public T GetWebSetting<T>(int sectionId, int? userId, string settingName, T defaulValue)
        {
            return GetSetting(sectionId, userId, settingName, defaulValue);
        }

        public void SetWebSetting<T>(int sectionId, int? userId, string settingName, T value)
        {
            var realKey = string.Format("Web_{0}", settingName);
            SettingRepository.Set(sectionId, userId, realKey, value);
            CacheWrapper.Put(realKey, value, CacheWrapper.GetDefaultSettingCacheTimeout);
        }

        private T GetSetting<T>(int sectionId, int? userId, string settingName, T defaultValue)
        {
            var realKey = string.Format("Web_{0}", settingName);

            if (CacheWrapper.Exists(realKey))
            {
                return CacheWrapper.Get<T>(realKey);
            }

            T result;
            var setting = SettingRepository.FindByName(sectionId, userId, realKey);
            if (setting == null)
                result = defaultValue;
            else
                result = (T)Convert.ChangeType(setting.Value, typeof(T));

            CacheWrapper.Put(realKey, result, CacheWrapper.GetDefaultSettingCacheTimeout);

            return result;
        }
    }
}