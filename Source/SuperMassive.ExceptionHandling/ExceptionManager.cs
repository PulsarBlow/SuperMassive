namespace SuperMassive.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using SuperMassive.ExceptionHandling.Properties;

    /// <summary>
    /// Exception Manager
    /// </summary>
    public class ExceptionManager : IExceptionManager
    {
        #region Members
        private readonly IEnumerable<IExceptionHandler> handlers;
        private readonly PostHandlingAction postHandlingAction;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a new <see cref="ExceptionManager"/>
        /// </summary>
        /// <param name="postHandlingAction"></param>
        /// <param name="handlers"></param>
        public ExceptionManager(PostHandlingAction postHandlingAction, IEnumerable<IExceptionHandler> handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
            }
            this.handlers = handlers;
            this.postHandlingAction = postHandlingAction;
        }
        #endregion

        #region Public Methods
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
        public bool HandleException(Exception exceptionToHandle)
        {
            if (exceptionToHandle == null)
                throw new ArgumentNullException("exceptionToHandle");
            Exception chainException = ExecuteHandlerChain(exceptionToHandle);
            return RethrowRecommanded(chainException, exceptionToHandle);
        }
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
        public bool HandleException(Exception exceptionToHandle, out Exception exceptionToThrow)
        {
            try
            {
                bool shouldRethrow = HandleException(exceptionToHandle);
                exceptionToThrow = null;
                return shouldRethrow;
            }
            catch (Exception exception)
            {
                exceptionToThrow = exception;
                return true;
            }
        }
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
        public void Process(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            try
            {
                action();
            }
            catch (Exception exception)
            {
                if (HandleException(exception))
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Executes the supplied delegate <paramref name="action"/>, and handles any thrown exception.
        /// </summary>
        /// <typeparam name="TResult">Type of return value from <paramref name="action"/>.</typeparam>
        /// <param name="action">The delegate to execute.</param>
        /// <param name="defaultResult">The value to return if an exception is thrown and the
        /// exception policy swallows it instead of rethrowing.</param>
        /// <returns>If no exception occurs, returns the result from executing <paramref name="action"/>. If
        /// an exception occurs and the policy does not re-throw, returns <paramref name="defaultResult"/>.</returns>
        public TResult Process<TResult>(Func<TResult> action, TResult defaultResult)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            try
            {
                return action();
            }
            catch (Exception exception)
            {
                if (HandleException(exception))
                {
                    throw;
                }
            }
            return defaultResult;
        }
        #endregion

        #region Private Methods
        private Exception ExecuteHandlerChain(Exception ex)
        {
            if (this.handlers == null || !this.handlers.Any())
                return ex;

            string lastHandlerName = String.Empty;

            try
            {
                foreach (var handler in this.handlers)
                {
                    lastHandlerName = handler.Name;
                    ex = handler.HandleException(ex);
                }
            }
            catch (Exception handlingException)
            {
                throw new ExceptionHandlingException(String.Format(CultureInfo.CurrentCulture, Resources.UnableToHandleException, lastHandlerName), handlingException);
            }
            return ex;
        }

        private bool RethrowRecommanded(Exception chainException, Exception originalException)
        {
            if (postHandlingAction == PostHandlingAction.None)
                return false;
            if (postHandlingAction == PostHandlingAction.ThrowNewException)
            {
                throw IntentionalRethrow(chainException, originalException);
            }
            return true;
        }
        /// <summary>
        /// Rethrows the given exception.  Placed in a seperate method for
        /// easier viewing in the stack trace.
        /// </summary>
        /// <param name="chainException"></param>
        /// <param name="originalException"></param>
        /// <returns></returns>
        private Exception IntentionalRethrow(Exception chainException, Exception originalException)
        {
            if (chainException != null)
            {
                throw chainException;
            }

            Exception wrappedException = new ExceptionHandlingException(Resources.ExceptionNullException, originalException);
            return wrappedException;
        }
        #endregion
    }
}
