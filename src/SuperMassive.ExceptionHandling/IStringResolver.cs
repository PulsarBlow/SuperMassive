
namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// Resolves string objects. 
    /// </summary>
    public interface IStringResolver
    {
        /// <summary>
        /// Returns a string represented by the receiver.
        /// </summary>
        /// <returns>The string object.</returns>        
        string GetString();
    }
}
