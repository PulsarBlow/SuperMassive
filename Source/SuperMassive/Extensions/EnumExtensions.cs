#nullable enable

namespace SuperMassive.Extensions
{
    using System;

    public static class EnumExtensions
    {
        /// <summary>
        /// Includes an enumerated type and returns the new value
        /// </summary>
        // TODO: Rename Include to Add and make Include obsolete
        public static T Include<T>(this Enum @enum, T value)
            where T : struct
        {
            var type = @enum.GetType();
            object result = @enum;

            var parsed = new NarrowedValue(value, type);
            if (parsed.Signed != null)
            {
                result = Convert.ToInt64(@enum) | parsed.Signed.Value;
            }
            else if (parsed.Unsigned != null)
            {
                result = Convert.ToUInt64(@enum) | parsed.Unsigned.Value;
            }

            return (T) Enum.Parse(type, result.ToString()!);
        }

        /// <summary>
        /// Removes an enumerated type and returns the new value
        /// </summary>
        public static T Remove<T>(this Enum @enum, T value)
            where T : struct
        {
            var type = @enum.GetType();
            object result = @enum;

            var parsed = new NarrowedValue(value, type);
            if (parsed.Signed != null)
            {
                result = Convert.ToInt64(@enum) & ~parsed.Signed;
            }
            else if (parsed.Unsigned != null)
            {
                result = Convert.ToUInt64(@enum) & ~parsed.Unsigned;
            }

            return (T) Enum.Parse(type, result.ToString()!);
        }

        /// <summary>
        /// Checks if an enumerated type contains a value
        /// </summary>
        public static bool Has<T>(this Enum @enum, T value)
            where T : struct
        {
            var type = @enum.GetType();

            var parsed = new NarrowedValue(value, type);
            if (parsed.Signed != null)
            {
                return (Convert.ToInt64(@enum) &
                        (long) parsed.Signed) == parsed.Signed;
            }

            if (parsed.Unsigned != null)
            {
                return (Convert.ToUInt64(@enum) &
                        (ulong) parsed.Unsigned) == parsed.Unsigned;
            }

            return false;
        }

        /// <summary>
        /// Checks if an enumerated type is missing a value
        /// </summary>
        // TODO: Rename Missing to HasNot and make Missing obsolete
        public static bool Missing<T>(this Enum @enum, T value)
            where T : struct
        {
            return !Has(@enum, value);
        }

        // Class to simplify narrowing values between a ulong and long since either value should cover any lesser value
        private class NarrowedValue
        {
            // Cached comparisons for type to use
            private static readonly Type UnsignedLong = typeof(ulong);
            private static readonly Type SignedLong = typeof(long);

            public readonly long? Signed;
            public readonly ulong? Unsigned;

            public NarrowedValue(object value, Type type)
            {
                // Make sure it is even an enum to work with
                Guard.Requires<ArgumentException>(() => type.IsEnum, "isEnum_type_mismatch", nameof(type));

                // Then check for the enumerated value
                var compare = Enum.GetUnderlyingType(type);

                // If this is an unsigned long then the only
                // value that can hold it would be a ulong
                if (compare == SignedLong || compare == UnsignedLong)
                {
                    Unsigned = Convert.ToUInt64(value);
                }
                //otherwise, a long should cover anything else
                else
                {
                    Signed = Convert.ToInt64(value);
                }
            }
        }
    }
}
