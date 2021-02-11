using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace UITMBER
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        public const string SERVERENDPOINT = "https://dev.wsiz.edu.pl";


        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string AccessTokenKey = "AccessToken_key";
        private const string RolesKey = "RolesKey_key";
        private const string NameKey = "NameKey_key";
        private const string PhotoKey = "PhotoKey_key";
        private const string TokenExpireKey = "TokenExpireKey_key";

        #endregion


        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }


        public static string Roles
        {
            get
            {
                return AppSettings.GetValueOrDefault(RolesKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RolesKey, value);
            }
        }


        public static string Name
        {
            get
            {
                return AppSettings.GetValueOrDefault(NameKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(NameKey, value);
            }
        }

        public static string Photo
        {
            get
            {
                return AppSettings.GetValueOrDefault(PhotoKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PhotoKey, value);
            }
        }

        public static DateTime TokenExpire
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenExpireKey, DateTime.MinValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TokenExpireKey, value);
            }
        }
        
    }
}
