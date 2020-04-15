#nullable enable

namespace SuperMassive
{
    /// <summary>
    /// Transience interface
    /// </summary>
    public interface ITransient
    {
        /// <summary>
        /// Returns true if the current object state is transient
        /// </summary>
        bool IsTransient { get; }
    }
}
