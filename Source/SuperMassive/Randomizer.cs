using System;
using System.Text;

namespace SuperMassive
{
    /// <summary>
    /// Provides helping methods to generate random stuff
    /// </summary>
    public static class Randomizer
    {
        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length = 64)
        {
            if (length == 64)
                return CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString());
            else if (length < 64)
                return CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()).Substring(0, length);

            int number = (int)Math.Ceiling((double)length / 64.0);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < number; i++)
                sb.Append(CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString()));
            return sb.ToString().Substring(0, length);
        }
    }
}
