namespace SuperMassive
{
    using System;

    /// <summary>
    /// A composite identifier made of a Guid and a Timestamp which
    /// string representation can be lexicographicaly ordered.
    /// <para>Credits to : https://github.com/hatem-b</para>
    /// </summary>
    public struct AscendingSortedGuid : IComparable, IComparable<AscendingSortedGuid>, IEquatable<AscendingSortedGuid>
    {
        /// <summary>
        /// A read-only instance of the AscendingSortedGuid structure whose value is all zeros.
        /// </summary>
        public static readonly AscendingSortedGuid Empty = new AscendingSortedGuid();

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="guid"></param>
        public AscendingSortedGuid(DateTimeOffset timestamp, Guid guid)
        {
            Guard.ArgumentNotNull(timestamp, "timestamp");
            Guard.ArgumentNotNull(guid, "guid");
            this.Timestamp = timestamp;
            this.Guid = guid;
        }

        /// <summary>
        /// Parse a string value into an <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <param name="id">Should be in the form of 0000000000000000000_00000000000000000000000000000000</param>
        /// <returns></returns>

        public static AscendingSortedGuid Parse(string id)
        {
            Guard.ArgumentNotNullOrEmpty(id, "id");
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid", nameof(id)); }

            AscendingSortedGuid result = new AscendingSortedGuid();

            var splits = id.Split(SortedGuidHelper.TokenSeparator);

            string inversedDate = splits[0];
            string guid = splits[1];
            long date = long.Parse(inversedDate);

            result.Guid = Guid.Parse(guid);
            result.Timestamp = new DateTimeOffset(date, new TimeSpan(0));

            return result;
        }

        /// <summary>
        /// Try to parse a string value into a <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns>Returns true if parsing succeeded, otherwise false</returns>
        public static bool TryParse(string id, out AscendingSortedGuid result)
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
        /// Creates a new <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <returns></returns>
        public static AscendingSortedGuid NewSortedGuid()
        {
            return new AscendingSortedGuid(
                DateTimeOffset.UtcNow,
                Guid.NewGuid());
        }

        /// <summary>
        /// Returns the string representation of the <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SortedGuidHelper.GetFormatedString(Timestamp.Ticks, Guid);
        }

        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns></returns>
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
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AscendingSortedGuid))
                return false;
            return Equals((AscendingSortedGuid)obj);
        }

        /// <summary>
        /// Returns true if the given <see cref="AscendingSortedGuid"/> is equal to the given instance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AscendingSortedGuid other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.Timestamp != other.Timestamp)
                return false;
            if (this.Guid != other.Guid)
                return false;
            return true;
        }

        /// <summary>
        /// Equality operator overloading
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            if (value1.Timestamp != value2.Timestamp)
            {
                return false;
            }
            if (value1.Guid != value2.Guid)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Equality operator overloading
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            return !(value1 == value2);
        }

        public static bool operator >(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            return value1.CompareTo(value2) == 1;
        }

        public static bool operator >=(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            int result = value1.CompareTo(value2);
            return (result == 0 || result == 1);
        }

        public static bool operator <(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            return value1.CompareTo(value2) == -1;
        }

        public static bool operator <=(AscendingSortedGuid value1, AscendingSortedGuid value2)
        {
            int result = value1.CompareTo(value2);
            return (result == 0 || result == -1);
        }

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
            if (value == null)
                return -1;

            if (!(value is AscendingSortedGuid))
                return -1;

            return CompareTo((AscendingSortedGuid)value);

        }

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AscendingSortedGuid other)
        {
            if (other == null)
            {
                return 1;
            }

            if (this.Timestamp < other.Timestamp)
                return -1;
            if (this.Timestamp > other.Timestamp)
                return 1;
            // Timestamp are equal, check guid now
            return this.Guid.CompareTo(other.Guid);
        }
    }
}
