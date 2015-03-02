using System;

namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// ExceptionManager Interface
    /// </summary>
    public interface IExceptionManager
    {
        /// <summary>
        /// Handles the specified <see cref="Exception"/>
        /// </summary>
        /// <param name="exceptionToHandle">An <see cref="Exception"/> object.</param>
        /// <returns>
        /// Whether or not a rethrow is recommended.
        /// </returns>
        /// <example>
        /// The following code shows the usage of the 
        /// exception handling framework.
        /// <code>
        /// try
        ///	{
        ///		DoWork();
        ///	}
        ///	catch (Exception e)
        ///	{
        ///		if (exceptionManager.HandleException(e)) throw;
        ///	}
        /// </code>
        /// </example>
        bool HandleException(Exception exceptionToHandle);
        /// <summary>
        /// Handles the specified <see cref="Exception"/>
        /// </summary>
        /// <param name="exceptionToHandle">An <see cref="Exception"/> object.</param>
        /// <param name="exceptionToThrow">The new <see cref="Exception"/> to throw, if any.</param>
        /// <remarks>
        /// If a rethrow is recommended and <paramref name="exceptionToThrow"/> is <see langword="null"/>,
        /// then the original exception <paramref name="exceptionToHandle"/> should be rethrown; otherwise,
        /// the exception returned in <paramref name="exceptionToThrow"/> should be thrown.
        /// </remarks>
        /// <returns>
        /// Whether or not a rethrow is recommended. 
        /// </returns>
        /// <example>
        /// The following code shows the usage of the 
        /// exception handling framework.
        /// <code>
        /// try
        ///	{
        ///		DoWork();
        ///	}
        ///	catch (Exception e)
        ///	{
        ///	    Exception exceptionToThrow;
        ///		if (exceptionManager.HandleException(e, out exceptionToThrow))
        ///		{
        ///		  if(exceptionToThrow == null)
        ///		    throw;
        ///		  else
        ///		    throw exceptionToThrow;
        ///		}
        ///	}
        /// </code>
        /// </example>
        bool HandleException(Exception exceptionToHandle, out Exception exceptionToThrow);
        /// <summary>
        /// Excecutes the supplied delegate <paramref name="action"/> and handles any thrown exception.
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        /// <example>
        /// The following code shows the usage of this method.
        /// <code>
        ///		exceptionManager.Process(() => { DoWork(); });
        /// </code>
        /// </example>
        void Process(Action action);
        /// <summary>
        /// Executes the supplied delegate <paramref name="action"/>, and handles any thrown exception.
        /// </summary>
        /// <typeparam name="TResult">Type of return value from <paramref name="action"/>.</typeparam>
        /// <param name="action">The delegate to execute.</param>
        /// <param name="defaultResult">The value to return if an exception is thrown and the
        /// exception policy swallows it instead of rethrowing.</param>
        /// <returns>If no exception occurs, returns the result from executing <paramref name="action"/>. If
        /// an exception occurs and the policy does not re-throw, returns <paramref name="defaultResult"/>.</returns>
        TResult Process<TResult>(Func<TResult> action, TResult defaultResult);
    }
}
