namespace SuperMassive
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define an exception which occurs when any data validation process fails
    /// </summary>
    public class ValidationFailedException : Exception
    {
        /// <summary>
        /// A collection of ValidationResult (broken rules) associated with this exception
        /// </summary>
        public ValidationResultCollection? ValidationResults
        {
            get;
            protected set;
        }

        /// <summary>
        /// Instantiates a new <see cref="ValidationFailedException"/>
        /// </summary>
        public ValidationFailedException()
            : base()
        { }
        /// <summary>
        /// Instantiates a new <see cref="ValidationFailedException"/>
        /// </summary>
        /// <param name="message"></param>
        public ValidationFailedException(string message)
            : base(message)
        { }
        /// <summary>
        /// Instantiates a new <see cref="ValidationFailedException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationResults"></param>
        public ValidationFailedException(string message, IEnumerable<ValidationResult> validationResults)
            : base(message)
        {
            ValidationResults = new ValidationResultCollection(validationResults);
        }
        /// <summary>
        /// Instantiates a new <see cref="ValidationFailedException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ValidationFailedException(string message, Exception exception)
            : base(message, exception)
        { }
        /// <summary>
        /// Instantiates a new <see cref="ValidationFailedException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ValidationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
