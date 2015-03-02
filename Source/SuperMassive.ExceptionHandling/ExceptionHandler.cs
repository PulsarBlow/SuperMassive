using System;

namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// Base class for Exception Handler implementations
    /// </summary>
    public abstract class ExceptionHandler : IExceptionHandler
    {
        private readonly string name;
        /// <summary>
        /// Handler name
        /// </summary>
        public string Name
        {
            get
            {
                if (String.IsNullOrWhiteSpace(this.name))
                    return this.GetType().Name;
                return this.name;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class with the name is the handler.
        /// </summary>
        /// <param name="name"></param>
        public ExceptionHandler(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            this.name = name;
        }
        /// <summary>
        /// <para>When implemented by a class, handles an <see cref="Exception"/>.</para>
        /// </summary>
        /// <param name="exception"><para>The exception to handle.</para></param>        
        /// <returns><para>Modified exception to pass to the next exceptionHandlerData in the chain.</para></returns>
        public abstract Exception HandleException(Exception exception);
    }
}
