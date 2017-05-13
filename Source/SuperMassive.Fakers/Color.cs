namespace SuperMassive.Fakers
{
    /// <summary>
    /// Color Faker
    /// </summary>
    public static class Color
    {
        /// <summary>
        /// Generates a random <see cref="System.Drawing.Color"/>
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Color DrawingColor()
        {
            return System.Drawing.Color.FromName(DataStore.ColorNames.RandPick());
        }

        /// <summary>
        /// Generates a random system <see cref="System.Drawing.Color"/>
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Color SystemColor()
        {
            System.Drawing.Color color = System.Drawing.Color.Empty;
            do
            {
                string randomColorName = DataStore.ColorNames.RandPick();
                color = System.Drawing.Color.FromName(randomColorName);
            }
            while (color.IsSystemColor);
            return color;
        }

        /// <summary>
        /// Generates a random color as a ARGB integer value
        /// </summary>
        /// <returns>A random color as a ARGB integer value</returns>
        public static int ColorAsInteger()
        {
            System.Drawing.Color color = System.Drawing.Color.Empty;
            do
            {
                string randomColorName = DataStore.ColorNames.RandPick();
                color = System.Drawing.Color.FromName(randomColorName);
            }
            while (color.IsSystemColor);

            return color.ToArgb();
        }
    }
}
