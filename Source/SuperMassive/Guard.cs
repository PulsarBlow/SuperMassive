#nullable enable

namespace SuperMassive
{
    using System;
    using System.Globalization;
    using Properties;

    /// <summary>
    /// A static helper class that includes various parameter checking routines.
    /// Guard provides a way to specify preconditions in your code.
    /// Preconditions are requirements that must be met when entering a method or property.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given argument is null.
        /// </summary>
        /// <param name="argumentValue">Argument value to test.</param>
        /// <param name="argumentName">Name of the argument being tested.</param>
        /// <exception cref="ArgumentNullException">If argument value is null</exception>
        public static void ArgumentNotNull(object? argumentValue, string argumentName)
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
        /// <param name="argumentValue">The argument value</param>
        /// <param name="argumentName">The argument name</param>
        public static void ArgumentNotNull<TWrapException>(object? argumentValue, string argumentName)
            where TWrapException : Exception, new()
        {
            if (argumentValue != null)
                return;

            WrapAndThrow<TWrapException, ArgumentNullException>(
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_ARGUMENT_ISNULL_WITHFORMAT,
                    argumentName),
                () => new ArgumentNullException(argumentName),
                argumentName);
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
            ArgumentNotNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_ARGUMENT_ISEMPTY_WITHFORMAT,
                        argumentName),
                    argumentName);
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
            ArgumentNotNull<TWrapException>(argumentValue, argumentName);

            if (argumentValue.Length == 0)
            {
                WrapAndThrow<TWrapException>(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_ARGUMENT_ISEMPTY_WITHFORMAT,
                        argumentName));
            }
        }

        /// <summary>
        /// Throws an exception if the tested string argument is null, empty or only composed of white spaces.
        /// </summary>
        /// <param name="argumentValue">Argument value to check.</param>
        /// <param name="argumentName">Name of argument being checked.</param>
        /// <exception cref="ArgumentNullException">Thrown if the string is null</exception>
        /// <exception cref="ArgumentException">Thrown if the string is empty or contains only whitespaces.</exception>
        public static void ArgumentNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            ArgumentNotNullOrEmpty(argumentValue, argumentName);

            if (argumentValue.Trim().Length == 0)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_ARGUMENT_MUSTNOTBE_WHITESPACE,
                        argumentName),
                    argumentName);
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

            if (argumentValue.Trim().Length == 0)
            {
                WrapAndThrow<TWrapException>(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_ARGUMENT_ISEMPTY_WITHFORMAT,
                    argumentName));
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given precondition is not fulfilled
        /// </summary>
        /// <param name="precondition">A delegated operation which returns a boolean</param>
        /// <param name="preconditionName">The name of the precondition. Will be use to format the exception message</param>
        /// <param name="argumentName">Argument name which must fulfill the precondition</param>
        /// <exception cref="ArgumentException">Occurs when the precondition is not satisfied</exception>
        public static void Requires(Func<bool> precondition, string? preconditionName = null, string? argumentName = null)
        {
            ArgumentNotNull(precondition, nameof(precondition));

            if (!precondition.Invoke())
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT,
                        string.IsNullOrWhiteSpace(preconditionName) ? string.Empty : preconditionName,
                        string.IsNullOrWhiteSpace(argumentName) ? string.Empty : argumentName),
                    argumentName);
            }
        }

        /// <summary>
        /// Throws an <see cref="TWrapException"/> if the given precondition is not fulfilled
        /// </summary>
        /// <typeparam name="TWrapException">The exception type to wrap the thrown exception with</typeparam>
        /// <param name="precondition">A delegated operation which returns a boolean</param>
        /// <param name="preconditionName">The name of the precondition. Will be use to format the exception message</param>
        /// <param name="argumentName">Argument name which must fulfill the precondition</param>
        /// <exception cref="ArgumentNullException">Occurs when the precondition is null</exception>
        /// <exception cref="ArgumentException">Occurs when the precondition is not satisfied</exception>
        public static void Requires<TWrapException>(Func<bool> precondition, string? preconditionName = null, string? argumentName = null)
            where TWrapException : Exception, new()
        {
            ArgumentNotNull<TWrapException>(precondition, nameof(precondition));

            if (!precondition.Invoke())
            {
                WrapAndThrow<TWrapException>(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_PRECONDITION_WITHFORMAT,
                    string.IsNullOrWhiteSpace(preconditionName) ? string.Empty : preconditionName,
                    string.IsNullOrWhiteSpace(argumentName) ? string.Empty : argumentName));
            }
        }

        /// <summary>
        /// Verifies that an argument instance is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy,
        /// or instance can be assigned through a runtime wrapper, as is the case for
        /// COM Objects).
        /// </summary>
        /// <param name="targetType">The argument type that will be assigned to.</param>
        /// <param name="instance">The instance that will be assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <exception cref="ArgumentNullException">if the tested type is null</exception>
        /// <exception cref="ArgumentNullException">if the tested object instance is null</exception>
        /// <exception cref="ArgumentException">if tested instance is not assignable</exception>
        public static void InstanceIsAssignable(Type targetType, object instance, string argumentName)
        {
            ArgumentNotNull(targetType, nameof(targetType));
            ArgumentNotNull(instance, nameof(instance));

            if (!targetType.IsInstanceOfType(instance))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_TYPE_ISNOTASSIGNABLE_WITHFORMAT,
                        targetType,
                        GetTypeName(instance)),
                    argumentName);
            }
        }

        /// <summary>
        /// Verifies that an argument instance is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy,
        /// or instance can be assigned through a runtime wrapper, as is the case for
        /// COM Objects).
        /// </summary>
        /// <typeparam name="TWrapException">The exception type to wrap the thrown exception with</typeparam>
        /// <param name="targetType">The argument type that will be assigned to.</param>
        /// <param name="instance">The instance that will be assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <exception cref="ArgumentNullException">if the tested type is null</exception>
        /// <exception cref="ArgumentNullException">if the tested object instance is null</exception>
        /// <exception cref="ArgumentException">if tested instance is not assignable</exception>
        public static void InstanceIsAssignable<TWrapException>(Type targetType, object instance, string argumentName)
            where TWrapException : Exception, new()
        {
            ArgumentNotNull<TWrapException>(targetType, nameof(targetType));
            ArgumentNotNull<TWrapException>(instance, nameof(instance));

            if (!targetType.IsInstanceOfType(instance))
            {
                WrapAndThrow<TWrapException>(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_TYPE_ISNOTASSIGNABLE_WITHFORMAT,
                    targetType,
                    GetTypeName(instance)),
                    argumentName);
            }
        }

        /// <summary>
        /// Verifies that an argument type is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy).
        /// </summary>
        /// <param name="targetType">The argument type that will be assigned to.</param>
        /// <param name="valueType">The type of the value being assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void TypeIsAssignable(Type targetType, Type valueType, string argumentName)
        {
            ArgumentNotNull(targetType, nameof(targetType));
            ArgumentNotNull(valueType, nameof(valueType));

            if (!targetType.IsAssignableFrom(valueType))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_TYPE_ISNOTASSIGNABLE_WITHFORMAT,
                        targetType,
                        valueType),
                    argumentName);
            }
        }

        /// <summary>
        /// Verifies that an argument type is assignable from the provided type (meaning
        /// interfaces are implemented, or classes exist in the base class hierarchy).
        /// </summary>
        /// <typeparam name="TWrapException">The exception type to wrap the thrown exception with</typeparam>
        /// <param name="targetType">The argument type that will be assigned to.</param>
        /// <param name="valueType">The type of the value being assigned.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void TypeIsAssignable<TWrapException>(Type targetType, Type valueType, string argumentName)
            where TWrapException : Exception, new()
        {
            ArgumentNotNull<TWrapException>(targetType, nameof(targetType));
            ArgumentNotNull<TWrapException>(valueType, nameof(valueType));

            if (!targetType.IsAssignableFrom(valueType))
            {
                WrapAndThrow<TWrapException>(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GENERIC_GUARD_FAILURE_TYPE_ISNOTASSIGNABLE_WITHFORMAT,
                    targetType,
                    valueType),
                    argumentName);
            }
        }

        /// <summary>
        /// Verifies that an argument type is an expected type
        /// </summary>
        /// <param name="type">The expected type</param>
        /// <param name="argumentValue">The argument value</param>
        /// <param name="argumentName">The argument name</param>
        public static void IsInstanceOfType(Type type, object argumentValue, string argumentName)
        {
            ArgumentNotNull(type, nameof(type));
            ArgumentNotNull(argumentValue, nameof(argumentValue));

            if (!type.IsInstanceOfType(argumentValue))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GENERIC_GUARD_FAILURE_INSTANCE_ISNOTOFTYPE_WITHFORMAT,
                        type),
                    argumentName);
            }
        }

        /// <summary>
        /// Gets the type name of the given object instance
        /// </summary>
        /// <param name="assignmentInstance">The instance used to find the type name</param>
        /// <returns>The type name as a string</returns>
        private static string GetTypeName(object assignmentInstance)
        {
            try
            {
                return assignmentInstance.GetType().FullName ?? string.Empty;
            }
            catch (Exception)
            {
                return Resources.GENERIC_GUARD_FAILURE_UNKNOWNTYPE;
            }
        }

        private static void WrapAndThrow<TWrapException, TInnerException>(string message, Func<TInnerException> innerActivator, string? argumentName = null)
            where TWrapException : Exception, new()
            where TInnerException : Exception, new()
        {
            if (Activator.CreateInstance(typeof(TWrapException), message,
                innerActivator.Invoke()) is TWrapException instance)
                throw instance;
            throw new ArgumentException(message, argumentName);
        }

        private static void WrapAndThrow<TWrapException>(string message, string? argumentName = null)
            where TWrapException : Exception, new()
        {
            if (Activator.CreateInstance(typeof(TWrapException), message,
                new ArgumentException(message, argumentName)) is TWrapException instance)
                throw instance;
            throw new ArgumentException(message, argumentName);
        }
    }
}
