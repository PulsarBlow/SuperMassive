using System;
using System.Runtime.Serialization;

namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// An error occured during exception handling
    /// </summary>
    [Serializable]
    public class ExceptionHandlingException : Exception
    {
        /// <summary>
        /// Instantiates a new <see cref="ExceptionHandlingException"/>
        /// </summary>
        public ExceptionHandlingException()
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ExceptionHandlingException"/>
        /// </summary>
        /// <param name="message"></param>
        public ExceptionHandlingException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ExceptionHandlingException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ExceptionHandlingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ExceptionHandlingException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ExceptionHandlingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
