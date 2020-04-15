#nullable enable

namespace SuperMassive
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// String manipulation helping methods
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Removes accents from a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The result string</returns>
        public static string RemoveDiacritics(string value)
        {
            var formD = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var @char in formD)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(@char);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(@char);
                }
            }
            return sb
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Encode a string in Base64 (UTF8)
        /// </summary>
        /// <param name="decodedValue"></param>
        /// <returns></returns>
        // TODO: Make obsolete in place of CryptographyHelper.EncodeBase64
        public static string Base64Utf8Encode(string decodedValue)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(decodedValue));
        }

        /// <summary>
        /// Decode a base64 string (UTF8)
        /// </summary>
        /// <param name="encodedValue"></param>
        /// <returns></returns>
        // TODO: Make obsolete in place of CryptographyHelper.DecodeBase64
        public static string? Base64Utf8Decode(string encodedValue)
        {
            if (string.IsNullOrEmpty(encodedValue))
                return null;

            UTF8Encoding encoder = new UTF8Encoding();
            Decoder decoder = encoder.GetDecoder();
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(encodedValue);
            }
            catch (FormatException)
            {
                return null;
            }

            int charCount = decoder.GetCharCount(bytes, 0, bytes.Length);
            char[] decodedChar = new char[charCount];
            decoder.GetChars(bytes, 0, bytes.Length, decodedChar, 0);
            return new string(decodedChar);
        }

        /// <summary>
        /// Capitalize the first letter of a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Capitalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}{1}",
                value[0].ToString().ToUpper(CultureInfo.CurrentCulture),
                value.Substring(1, value.Length - 1));
        }

        /// <summary>
        /// Returns a converted camel cased string into a string delimited by dashes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>StringHelper.Dasherize("dataRate"); //"data-rate"</example>
        /// <example>StringHelper.Dasherize("CarSpeed"); //"-car-speed"</example>
        /// <example>StringHelper.Dasherize("YesWeCan"); //"yes-we-can"</example>
        // Inspired by http://stringjs.com/#methods/dasherize
        public static string Dasherize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ?
                value :
                Regex.Replace(
                    value,
                    @"\p{Lu}",
                    m => "-" + m.ToString().ToLower(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Remove any underscores or dashes and convert a string into camel casing.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>StringHelper.Camelize("data_rate"); //"dataRate"</example>
        /// <example>StringHelper.Camelize("background-color"); //"backgroundColor"</example>
        /// <example>StringHelper.Camelize("-webkit-something"); //"WebkitSomething"</example>
        /// <example>StringHelper.Camelize("_car_speed"); //"CarSpeed"</example>
        /// <example>StringHelper.Camelize("yes_we_can"); //"yesWeCan"</example>
        // Inspired by http://stringjs.com/#methods/camelize
        public static string Camelize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ?
                value :
                Regex.Replace(
                    value,
                    @"[-_]\p{L}",
                    m => m.ToString().ToUpper(CultureInfo.CurrentCulture))
                    .Replace("-", string.Empty)
                    .Replace("_", string.Empty);
        }

        /// <summary>
        /// Remove extra white space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>StringHelper.CollapseWhiteSpaces("String value"); // "String value"</example>
        /// <example>StringHelper.CollapseWhiteSpaces(" String value "); // " String value "</example>
        /// <example>StringHelper.CollapseWhiteSpaces("String   value"); // "String value"</example>
        /// <example>StringHelper.CollapseWhiteSpaces("  String  value  "); // " String value "</example>
        /// <example>StringHelper.CollapseWhiteSpaces("String value  "); // "String value "</example>
        /// <example>StringHelper.CollapseWhiteSpaces("  String value"); // " String value"</example>
        public static string CollapseWhiteSpaces(string value)
        {
            if (string.IsNullOrEmpty(value)) { return value; }
            var reg = new Regex(@" {2,}", RegexOptions.Compiled);
            return reg.Replace(value, " ");
        }
    }
}
