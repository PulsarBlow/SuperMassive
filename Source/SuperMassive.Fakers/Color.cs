
namespace SuperMassive.Fakers
{
    public static class Color
    {
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
