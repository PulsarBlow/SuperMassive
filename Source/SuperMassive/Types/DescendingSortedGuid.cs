#nullable enable

namespace SuperMassive
{
    using System;
    using Properties;

    /// A composite identifier made of a Guid and a Timestamp.
    /// Its string representation can be lexicographically ordered (REVERSE ORDER).
    /// Originally implemented by : https://github.com/hatem-b
    public struct DescendingSortedGuid : IComparable, IComparable<DescendingSortedGuid>, IEquatable<DescendingSortedGuid>
    {
        /// <summary>
        /// A read-only instance of the AscendingSortedGuidDescendingSortedGuid structure whose value is all zeros.
        /// </summary>
        public static readonly DescendingSortedGuid Empty = new DescendingSortedGuid();

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="DescendingSortedGuid"/>
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="guid"></param>
        public DescendingSortedGuid(DateTimeOffset timestamp, Guid guid)
        {
            Guard.ArgumentNotNull(timestamp, nameof(timestamp));
            Guard.ArgumentNotNull(guid, nameof(guid));

            Timestamp = timestamp;
            Guid = guid;
        }

        /// <summary>
        /// Parse a string value into a <see cref="DescendingSortedGuid"/>
        /// </summary>
        /// <param name="id">Should be in the form of 0000000000000000000_00000000000000000000000000000000</param>
        /// <returns></returns>
        public static DescendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrWhiteSpace(id, nameof(id));
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException(Resources.Validation_NotValidSortedGuid, nameof(id)); }


            var splits = id.Split(SortedGuidHelper.TokenSeparator);

            string invertedDate = splits[0];
            string guid = splits[1];
            long date = DateTime.MaxValue.Ticks - long.Parse(invertedDate);

            return new DescendingSortedGuid(
                new DateTimeOffset(date, new TimeSpan(0)),
                Guid.Parse(guid));
        }

        /// <summary>
        /// Try to parse a string value into a <see cref="DescendingSortedGuid"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns>Returns true if parsing succeeded, otherwise false</returns>
        public static bool TryParse(string id, out DescendingSortedGuid result)
        {
            result = Empty;
            if (string.IsNullOrWhiteSpace(id))
                return false;

            try
            {
                result = Parse(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new empty sorted guid
        /// </summary>
        /// <returns></returns>
        public static DescendingSortedGuid NewSortedGuid()
        {
            return new DescendingSortedGuid(
                DateTimeOffset.UtcNow,
                Guid.NewGuid());
        }

        /// <summary>
        /// Returns the string representation of this sorted guid
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SortedGuidHelper.GetFormatedString(
                DateTime.MaxValue.Ticks - Timestamp.Ticks,
                Guid);
        }

        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns>Returns the hashcode of the current <see cref="DescendingSortedGuid"/></returns>
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * SortedGuidHelper.HashcodeMultiplier) +
                Timestamp.GetHashCode().GetHashCode() +
                Guid.GetHashCode();
        }

        /// <summary>
        /// Checks equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is DescendingSortedGuid guid && Equals(guid);
        }

        /// <summary>
        /// Returns true if the given <see cref="DescendingSortedGuid"/> is equal to the given instance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DescendingSortedGuid other)
        {
            if (Timestamp != other.Timestamp)
                return false;

            return Guid == other.Guid;
        }

        /// <summary>
        /// Equality operator overloading
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            if (value1.Timestamp != value2.Timestamp)
            {
                return false;
            }

            return !(value1.Guid != value2.Guid);
        }

        /// <summary>
        /// Equality operator overloading
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            return !(value1 == value2);
        }

        public static bool operator >(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            return value1.CompareTo(value2) == -1;
        }

        public static bool operator >=(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            int result = value1.CompareTo(value2);
            return (result == 0 || result == -1);
        }

        public static bool operator <(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            return value1.CompareTo(value2) == 1;
        }

        public static bool operator <=(DescendingSortedGuid value1, DescendingSortedGuid value2)
        {
            int result = value1.CompareTo(value2);
            return (result == 0 || result == 1);
        }

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(object? value)
        {
            if (value == null)
                return 1;

            return !(value is DescendingSortedGuid) ? 1 : CompareTo((DescendingSortedGuid)value);
        }

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DescendingSortedGuid other)
        {
            if (Timestamp < other.Timestamp)
                return 1;
            if (Timestamp > other.Timestamp)
                return -1;
            // Timestamp are equal, check guid now
            return Guid.CompareTo(other.Guid);
        }
    }
}
