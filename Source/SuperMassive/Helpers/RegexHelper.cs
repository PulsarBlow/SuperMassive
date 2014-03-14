using System;
using System.Text.RegularExpressions;

namespace SuperMassive
{
    /// <summary>
    /// Provides regex utilities
    /// </summary>
    public static class RegexHelper
    {
        #region Public Methods
        /// <summary>
        /// SHA-256 Regex pattern
        /// </summary>
        public const string SHA256Pattern = @"^[a-fA-F\d]{64}$";
        /// <summary>
        /// MD5 Regex pattern
        /// </summary>
        public const string MD5Pattern = @"^[a-fA-F\d]{32}$";
        /// <summary>
        /// Email Regex pattern
        /// </summary>
        public const string EmailPattern =
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        /// <summary>
        /// Url Regex pattern
        /// </summary>
        public const string UrlPattern = @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$";
        /// <summary>
        /// Returns true if the given value is an SHA-256 hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSHA256Hash(string value)
        {
            return IsPatternMatch(value, SHA256Pattern);
        }
        /// <summary>
        /// Returns true if the given value is a MD5 hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMD5Hash(string value)
        {
            return IsPatternMatch(value, MD5Pattern);
        }
        /// <summary>
        /// Returns true if the given value is an email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            return IsPatternMatch(value, EmailPattern);
        }
        /// <summary>
        /// Returns true if the given value is a valid url
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(string value)
        {
            return IsPatternMatch(value, UrlPattern);
            //return Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute);
        }
        #endregion

        #region Private Methods
        private static bool IsPatternMatch(string value, string pattern, RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;
            if (String.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern");
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        #endregion
    }
}
