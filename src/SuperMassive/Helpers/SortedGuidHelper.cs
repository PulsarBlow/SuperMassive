namespace SuperMassive
{
    using System;
    using System.Globalization;

    public static class SortedGuidHelper
    {
        public const char TokenSeparator = '_';
        public const string Format = "{0:D19}{1}{2:N}";
        public const int HashcodeMultiplier = 907;

        public static string GetFormatedString(long ticks, Guid guid)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                Format,
                ticks,
                TokenSeparator,
                guid);
        }
    }
}
