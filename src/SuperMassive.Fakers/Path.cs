using System;
using System.Globalization;

namespace SuperMassive.Fakers
{
    public static class Path
    {
        /// <summary>
        /// Generate a random file name
        /// </summary>
        /// <param name="extension">Overrides random extension</param>
        /// <returns></returns>
        public static string FileName(string extension = null)
        {
            string fileName = System.IO.Path.GetRandomFileName();

            if (!String.IsNullOrWhiteSpace(extension))
                return String.Format(CultureInfo.InvariantCulture,
                "{0}.{1}",
                fileName.Remove(fileName.IndexOf('.')),
                extension.Replace(".", ""));

            return System.IO.Path.GetRandomFileName();
        }
    }
}
