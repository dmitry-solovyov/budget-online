using System;

namespace BudgetOnline.Common.Contracts
{
    public interface ISettingsHelper
    {
        TimeSpan PasswordValidityPeriod(int sectionId);
        TimeSpan TokenValidityPeriod(int sectionId);

        T GetWebSetting<T>(int sectionId, int? userId, string settingName, T defaulValue);
        void SetWebSetting<T>(int sectionId, int? userId, string settingName, T value);
    }

    public static class SettingsHelperConstants
    {
        public const string StatisticCacheActive = "StatisticCacheActive";
    }
}
