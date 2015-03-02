using System;

namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// ExceptionHandler Interface
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handler name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// <para>When implemented by a class, handles an <see cref="Exception"/>.</para>
        /// </summary>
        /// <param name="exception"><para>The exception to handle.</para></param>        
        /// <returns><para>Modified exception to pass to the next exceptionHandlerData in the chain.</para></returns>
        Exception HandleException(Exception exception);
    }
}
