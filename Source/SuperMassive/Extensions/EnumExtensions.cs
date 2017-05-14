namespace SuperMassive.Extensions
{
    using System;

    public static class EnumExtensions
    {
        /// <summary>
        /// Includes an enumerated type and returns the new value
        /// </summary>
        public static T Include<T>(this Enum extended, T item)
        {
            Type type = extended.GetType();

            //determine the values
            object result = extended;
            NarrowedValue parsed = new NarrowedValue(item, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(extended) | (long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(extended) | (ulong)parsed.Unsigned;
            }

            //return the final value
            return (T)Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Removes an enumerated type and returns the new value
        /// </summary>
        public static T Remove<T>(this Enum extended, T item)
        {
            Type type = extended.GetType();

            //determine the values
            object result = extended;
            NarrowedValue parsed = new NarrowedValue(item, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(extended) & ~parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(extended) & ~parsed.Unsigned;
            }

            //return the final value
            return (T)Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Checks if an enumerated type contains a value
        /// </summary>
        public static bool Has<T>(this Enum extended, T check)
        {
            Type type = extended.GetType();

            NarrowedValue parsed = new NarrowedValue(check, type);
            if (parsed.Signed is long)
            {
                return (Convert.ToInt64(extended) &
                    (long)parsed.Signed) == parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                return (Convert.ToUInt64(extended) &
                    (ulong)parsed.Unsigned) == parsed.Unsigned;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an enumerated type is missing a value
        /// </summary>
        public static bool Missing<T>(this Enum extended, T value)
        {
            return !Has<T>(extended, value);
        }

        //class to simplify narrowing values between a ulong and long since either value should cover any lesser value
        private class NarrowedValue
        {
            //cached comparisons for tye to use
            private static Type _unsignedLong = typeof(ulong);
            private static Type _signedLong = typeof(long);

            public long? Signed;
            public ulong? Unsigned;

            public NarrowedValue(object value, Type type)
            {
                //make sure it is even an enum to work with
                Guard.Requires<ArgumentException>(() => type.IsEnum, "isEnum_type_mismatch", nameof(type));

                //then check for the enumerated value
                Type compare = Enum.GetUnderlyingType(type);

                //if this is an unsigned long then the only
                //value that can hold it would be a ulong
                if (compare.Equals(NarrowedValue._signedLong) || compare.Equals(NarrowedValue._unsignedLong))
                {
                    this.Unsigned = Convert.ToUInt64(value);
                }
                //otherwise, a long should cover anything else
                else
                {
                    this.Signed = Convert.ToInt64(value);
                }

            }

        }
    }
}
