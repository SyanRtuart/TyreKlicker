using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TyreKlicker.XF.Core.Helpers
{
    /// <summary>
    ///     This is the Settings static class that can be used in your Core solution or in any
    ///     of your client applications. All settings are laid out the same exact way with getters
    ///     and setters.
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string LastUsedEmail
        {
            get => AppSettings.GetValueOrDefault(LastEmailUsedKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(LastEmailUsedKey, value);
        }

        public static string AccessToken
        {
            get
            {
                var accessToken = AppSettings.GetValueOrDefault(AccessTokenKey, SettingsDefault);

                return Crypto.Decrypt(accessToken, GlobalSetting.Instance.EncryptionPassword);
            }
            set
            {
                var encryptAccessToken = Crypto.Encrypt(value, GlobalSetting.Instance.EncryptionPassword);

                AppSettings.AddOrUpdateValue(AccessTokenKey, encryptAccessToken);
            }
        }

        #region Setting Constants

        private const string LastEmailUsedKey = "last_email_key";
        private const string AccessTokenKey = "access_token_key";

        private static readonly string SettingsDefault = string.Empty;

        #endregion Setting Constants
    }
}