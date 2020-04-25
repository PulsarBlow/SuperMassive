namespace SuperMassive
{
    using System.Reflection;

    /// <summary>
    /// Provides helping methods for manipulating assemblies
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Returns the file version of the executing assembly
        /// </summary>
        /// <returns>The file version of the executing assembly</returns>
        public static string? GetFileVersion()
        {
            return GetFileVersion(typeof(AssemblyHelper).Assembly);
        }

        /// <summary>
        /// Returns the file version of a given assembly
        /// </summary>
        /// <param name="assembly">Assembly used to get the file version</param>
        /// <returns>The file version of the assembly</returns>
        public static string? GetFileVersion(Assembly assembly)
        {
            return assembly
                .GetCustomAttribute<AssemblyFileVersionAttribute>()?
                .Version;
        }

        /// <summary>
        /// Returns the informational version of the executing assembly.
        /// </summary>
        /// <returns>The informational version of the assembly</returns>
        public static string? GetInformationalVersion()
        {
            return GetInformationalVersion(typeof(AssemblyHelper).Assembly);
        }

        /// <summary>
        /// Returns the informational version of the given assembly.
        /// </summary>
        /// <param name="assembly">Assembly used to get the informational version</param>
        /// <returns>The informational version of the assembly</returns>
        public static string? GetInformationalVersion(Assembly assembly)
        {
            return assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;
        }
    }
}
