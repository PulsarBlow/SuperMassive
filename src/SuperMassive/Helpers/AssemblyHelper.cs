namespace SuperMassive
{
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Provides helping methods for manipulating assemblies
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Returns the informational version of the executing assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetInformationalVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        /// <summary>
        /// Returns the informational version of the given assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        // TODO: Fix Informational Version. Should comes from Assembly attributes
        public static string GetInformationalVersion(Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
        }
    }
}
