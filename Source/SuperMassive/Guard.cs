using System;
using System.Globalization;
using SuperMassive.Properties;

namespace SuperMassive
{
    /// <summary>
    /// A static helper class that includes various parameter checking routines.
    /// Guard provides a way to specify preconditions in your code.
    /// Preconditions are requirements that must be met when entering a method or property.
    /// </summary>
    public static class Guard
    {
        #region Public Methods
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given argument is null.
        /// </summary>
        /// <param name="argumentValue">Argument value to test.</param>
        /// <param name="argumentName">Name of the argument being tested.</param>
        /// <exception cref="ArgumentNullException">if tested value is null</exception>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> wrapped in another exception if the given argument is null
        /// </summary>
        /// <typeparam name="TWrapException">Exception type to wrap the <see cref="ArgumentNullException"/> type with</typeparam>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void ArgumentNotNull<TWrapException>(object argumentValue, string argumentName)
            where TWrapException : Exception, new()
        {
            if (argumentValue == null)
            {
                TWrapException exception = Activator.CreateInstance(
                    typeof(TWrapException),
                    "Argument " + argumentName + " is null.",
                    new ArgumentNullException(argumentName)) as TWrapException;
                throw exception;
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given precondition is not fulfilled
        /// </summary>
        /// <param name="precondition">A delegated operation which returns a boolean</param>
        /// <param name="preconditionName">The name of the precondition. Will be use to format the exception message</param>
        /// <param name="argumentName">Arugment name which must fulfill the precondition</param>
        /// <param name="message"></param>
        public static void Requires(Func<bool> precondition, string preconditionName = null, string argumentName = null)
        {
            if (!precondition.Invoke())
            {
                throw new ArgumentException(String.Format("Precondition{0} for argument{1} failed.",
                    String.IsNullOrWhiteSpace(preconditionName) ? "" : " " + preconditionName,
                    String.IsNullOrWhiteSpace(argumentName) ? "" : " " + argumentName
                ), argumentName);
            }
        }
        /// <summary>
        /// Throws an exception if the tested string argument is null or the empty string.
        /// </summary>
        /// <param name="argumentValue">Argument value to check.</param>
        /// <param name="argumentName">Name of argument being checked.</param>
        /// <exception cref="ArgumentNullException">Thrown if string value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the string is empty.</exception>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(Resources.ArgumentMustNotBeEmpty, argumentName);
            }
        }
        /// <summary>
        /// Throws an exception if the tested string argument is null or the empty string.
        /// </summary>
        /// <typeparam name="TWrapException">The exception type to wrap the thrown exception with</typeparam>
        /// <param name="argumentValue">Argument value to check.</param>
        /// <param name="argumentName">Name of argument being checked.</param>
        public static void ArgumentNotNullOrEmpty<TWrapException>(string argumentValue, string argumentName)
            where TWrapException : Exception, new()
        {
            if (argumentValue == null)
            {
                TWrapException exception = Activator.CreateInstance(
                    typeof(TWrapException),
                    "Argument " + argumentName + " is null.",
                    new ArgumentNullException(argumentName)) as TWrapException;
                throw exception;
            }
            if (argumentValue.Length == 0)
            {
                TWrapException exception = Activator.CreateInstance(
                    typeof(TWrapException),
                    "Argument " + argumentName + " must not be empty",
                    new ArgumentException(Resources.ArgumentMustNotBeEmpty, argumentName)) as TWrapException;
                throw exception;
            }
        }
        /// <summary>
        /// Throws an exception if the tested string argument is null, empty or only composed of white spaces.
        /// </summary>
        /// <param name="argumentValue">Argument value to check.</param>
        /// <param name="argumentName">Name of argument being checked.</param>
        /// <exception cref="ArgumentNullException">Thrown if string value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the string is empty or whitespaced.</exception>
        public static void ArgumentNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            ArgumentNotNullOrEmpty(argumentValue, argumentName);
            if (String.IsNullOrEmpty(argumentValue.Trim(' ')))
            {
                throw new ArgumentException(Resources.ArgumentMustNotBeWhiteSpace, argumentName);
            }
        }
        /// <summary>
        /// Throws an exception if the tested string argument is null, empty or only composed of white spaces.
        /// </summary>
        /// <typeparam name="TWrapException">The exception type to wrap the thrown exception with</typeparam>
        /// <param name="argumentValue">Argument value to check.</param>
        /// <param name="argumentName">Name of argument being checked.</param>
        public static void ArgumentNotNullOrWhiteSpace<TWrapException>(string argumentValue, string argumentName)
            where TWrapException : Exception, new()
        {
            ArgumentNotNullOrEmpty<TWrapException>(argumentValue, argumentName);
            if (String.IsNullOrEmpty(argumentValue.Trim(' ')))
            {
                TWrapException exception = Activator.CreateInstance(
                    typeof(TWrapException),
                    "Argument " + argumentName + " must not be empty",
                    new ArgumentException(Resources.ArgumentMustNotBeWhiteSpace, argumentName)) as TWrapException;
                throw exception;
            }
        }
        /// <summary>
        /// Verifies that an argument instance is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy,
        /// or instance can be assigned through a runtime wrapper, as is the case for
        /// COM Objects).
        /// </summary>
        /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
        /// <param name="assignmentInstance">The instance that will be assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <exception cref="ArgumentNullException">if the tested type is null</exception>
        /// <exception cref="ArgumentNullException">if the tested object instance is null</exception>
        /// <exception cref="ArgumentException">if tested instance is not assignable</exception>
        public static void InstanceIsAssignable(Type assignmentTargetType, object assignmentInstance, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentInstance == null)
            {
                throw new ArgumentNullException("assignmentInstance");
            }
            if (!assignmentTargetType.IsInstanceOfType(assignmentInstance))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, new object[] { assignmentTargetType, Guard.GetTypeName(assignmentInstance) }), argumentName);
            }
        }
        /// <summary>
        /// Verifies that an argument type is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy).
        /// </summary>
        /// <param name="assignmentTargetType">The argument type that will be assigned to.</param>
        /// <param name="assignmentValueType">The type of the value being assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void TypeIsAssignable(Type assignmentTargetType, Type assignmentValueType, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentValueType == null)
            {
                throw new ArgumentNullException("assignmentValueType");
            }
            if (!assignmentTargetType.IsAssignableFrom(assignmentValueType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, new object[] { assignmentTargetType, assignmentValueType }), argumentName);
            }
        }
        /// <summary>
        /// Verifies that an argument type is an expected type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="argumentName"></param>
        public static void IsInstanceOfType(Type type, object value, string argumentName)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");

            if (!type.IsInstanceOfType(value))
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Resources.IsNotInstanceOfType, type), argumentName);
            }
        }
        #endregion

        #region Private Methods
        private static string GetTypeName(object assignmentInstance)
        {
            try
            {
                return assignmentInstance.GetType().FullName;
            }
            catch (Exception)
            {
                return Resources.UnknownType;
            }
        }
        #endregion
    }
}
