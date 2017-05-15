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
        /// CRC32 Regex Pattern
        /// </summary>
        public const string CRC32Pattern = @"^[a-fA-F\d]{8}$";

        /// <summary>
        /// Email Regex pattern
        /// </summary>
        /// <remarks>As per HTML5 specification : http://www.w3.org/TR/html5/forms.html#valid-e-mail-address</remarks>
        public const string EmailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        /// <summary>
        /// Url Regex pattern
        /// </summary>
        public const string UrlPattern = @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$";

        /// <summary>
        /// Valid pattern for a Azure Table container name
        /// </summary>
        public const string TableContainerNamePattern = @"^[A-Za-z][A-Za-z0-9]{2,62}$";

        /// <summary>
        /// Valid pattern for Azure Blob container name
        /// </summary>
        public const string BlobContainerNamePattern = @"^(?-i)(?:[a-z0-9]|(?<=[a-z0-9])-(?=[a-z0-9])){3,63}$";

        /// <summary>
        /// Valid pattern for a Guid
        /// </summary>
        public const string GuidPattern = @"^[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}$";

        /// <summary>
        /// Valid pattern for a Sorted Guid (timestamped guid)
        /// </summary>
        /// <example>2012010100000000000-3e1a5b4546d888a79887a98a795623a4</example>
        public const string SortedGuidPattern = @"^[0-9]{19}_[A-F0-9]{32}$";

        /// <summary>
        /// Valid pattern for a Semantic Versioning 2.0.0 value
        /// http://semver.org/
        /// </summary>
        public const string SemverPattern = @"^(?'MAJOR'(?:0|(?:[1-9]\d*)))\.(?'MINOR'(?:0|(?:[1-9]\d*)))\.(?'PATCH'(?:0|(?:[1-9]\d*)))(?:-(?'prerelease'[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*))?(?:\+(?'build'[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*))?$";

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
        /// Returns true if the given value is a CRC32 hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCRC32Hash(string value)
        {
            return IsPatternMatch(value, CRC32Pattern);
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
        }

        /// <summary>
        /// Returns true if the given value is a valid Azure Table container name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTableContainerNameValid(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;

            if (value.Equals("$root"))
            {
                return true;
            }

            return IsPatternMatch(value, TableContainerNamePattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// Returns true if the given value is a valid Azure Blob container name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBlobContainerNameValid(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;

            return IsPatternMatch(value, BlobContainerNamePattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// Returns true if the given value is a valid <see cref="Guid"/> (string guid).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsGuid(string value)
        {
            return IsPatternMatch(value, GuidPattern);
        }

        /// <summary>
        /// Returns true if the given value is a valid Sorted guid (ascending-descending)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSortedGuid(string value)
        {
            return IsPatternMatch(value, SortedGuidPattern);
        }

        /// <summary>
        /// Returns true if the given value is a valid Semantic Versioning 2.0.0 value
        /// </summary>
        /// <param name="value">The value to be tested</param>
        /// <returns>Returns true if the value is a valid Semver 2.0.0 value</returns>
        public static bool IsSemver(string value)
        {
            return IsPatternMatch(value, SemverPattern, RegexOptions.Compiled);
        }

        #endregion

        #region Private Methods
        private static bool IsPatternMatch(string value, string pattern, RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;
            if (String.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern");
            return Regex.IsMatch(value, pattern, regexOptions);
        }
        #endregion
    }
}
