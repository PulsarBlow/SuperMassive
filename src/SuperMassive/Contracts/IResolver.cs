namespace SuperMassive
{
    /// <summary>
    /// Resolver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResolver<out T>
    {
        /// <summary>
        /// Resolve
        /// </summary>
        /// <returns></returns>
        T Resolve();
    }
}
