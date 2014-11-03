using System;
using System.Globalization;

namespace SuperMassive
{
    /// <summary>
    /// A composite identifier made of a Guid and a Timestamp.
    /// Its string representation can be lexicographicaly ordered.
    /// Originaly implemented by : https://github.com/hatem-b
    /// </summary>
    public struct AscendingSortedGuid : IComparable, IComparable<AscendingSortedGuid>, IEquatable<AscendingSortedGuid>
    {
        private const char Separator = '_';

        /// <summary>
        /// A read-only instance of the AscendingSortedGuid structure whose value is all zeros.
        /// </summary>
        public static readonly AscendingSortedGuid Empty;

        static AscendingSortedGuid()
        {
            AscendingSortedGuid.Empty = new AscendingSortedGuid();
        }

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTimeOffset Timestamp;

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid;

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
            if (!RegexHelper.IsSortedGuid(id)) { throw new ArgumentException("Not valid SortedGuid"); }

            AscendingSortedGuid result = new AscendingSortedGuid();

            var splits = id.Split(Separator);

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
            Guard.ArgumentNotNullOrWhiteSpace(id, "id");

            result = AscendingSortedGuid.NewSortedGuid();
            try
            {
                result = AscendingSortedGuid.Parse(id);
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
            AscendingSortedGuid result = new AscendingSortedGuid();

            result.Guid = Guid.NewGuid();
            result.Timestamp = DateTimeOffset.UtcNow;

            return result;
        }



        /// <summary>
        /// Returns the string representation of the <see cref="AscendingSortedGuid"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:D19}{1}{2:N}", Timestamp.Ticks, Separator, Guid);
        }

        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Timestamp.GetHashCode() | Guid.GetHashCode();
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

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
            if (value == null)
                return -1;

            Guard.IsInstanceOfType(typeof(AscendingSortedGuid), value, "value");

            return CompareTo((AscendingSortedGuid)value);

        }

        /// <summary>
        /// Compares the given value to the current structure
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AscendingSortedGuid other)
        {
            if (this.Timestamp < other.Timestamp)
                return -1;
            if (this.Timestamp > other.Timestamp)
                return 1;
            // Timestamp are equal, check guid now
            return this.Guid.CompareTo(other.Guid);
        }
    }
}
