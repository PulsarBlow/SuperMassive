namespace SuperMassive
{
    using System.Globalization;

    /// <summary>
    /// Provides helping methods for manipulating colors
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Convert an Hexadecimal color to its int value
        /// </summary>
        /// <param name="hexColor">Color format in hexa (#FFFFFF or FFFFFF)</param>
        /// <returns></returns>
        public static int HexToInt(string hexColor)
        {
            Guard.ArgumentNotNullOrEmpty(hexColor, nameof(hexColor));
            return int.Parse(
                hexColor.Replace("#", string.Empty),
                NumberStyles.HexNumber);
        }
        /// <summary>
        /// Convert an int color to its Hexadecimal value
        /// </summary>
        /// <param name="color"></param>
        /// <returns>An hex color without the hash, ex: FFFFFF</returns>
        public static string IntToHex(int color)
        {
            return color.ToString("X");
        }
    }
}
