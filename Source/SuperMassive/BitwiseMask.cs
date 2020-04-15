#nullable enable

namespace SuperMassive
{
    /// <summary>
    /// Encapsulate a numeric value and expose some bitwise operation to manipulate it.
    /// </summary>
    public class BitwiseMask
    {
        private long _value;

        /// <summary>
        /// Creates a new instance of the <see cref="BitwiseMask"/> class.
        /// </summary>
        public BitwiseMask()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="BitwiseMask"/> class.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask(short value)
            : this((long)value)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="BitwiseMask"/> class.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask(int value)
            : this((long)value)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="BitwiseMask"/> class.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask(long value)
        {
            _value = value;
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> instance contains the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Has(short value)
        {
            return Has((long)value);
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> instance contains the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Has(int value)
        {
            return Has((long)value);
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> instance contains the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Has(long value)
        {
            return (_value & value) == value;
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> value is strictly equals to the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Is(short value)
        {
            return Is((long)value);
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> value is strictly equals to the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Is(int value)
        {
            return Is((long)value);
        }

        /// <summary>
        /// Returns true if the current <see cref="BitwiseMask"/> value is strictly equals to the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Is(long value)
        {
            return _value == value;
        }

        /// <summary>
        /// Adds the given value to the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Add(short value)
        {
            return Add((long)value);
        }

        /// <summary>
        /// Adds the given value to the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Add(int value)
        {
            return Add((long)value);
        }

        /// <summary>
        /// Adds the given value to the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Add(long value)
        {
            _value |= value;
            return this;
        }

        /// <summary>
        /// Removes the given value from the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Remove(short value)
        {
            return Remove((long)value);
        }

        /// <summary>
        /// Removes the given value from the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Remove(int value)
        {
            return Remove((long)value);
        }

        /// <summary>
        /// Removes the given value from the <see cref="BitwiseMask"/>.
        /// </summary>
        /// <param name="value"></param>
        public BitwiseMask Remove(long value)
        {
            _value &= ~value;
            return this;
        }

        /// <summary>
        /// Returns true if the <see cref="BitwiseMask"/> instance is empty (internal value = 0)
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _value == 0;
        }

        /// <summary>
        /// Resets the current <see cref="BitwiseMask"/> instance/
        /// </summary>
        /// <returns></returns>
        public BitwiseMask Reset()
        {
            _value = 0;
            return this;
        }
    }
}
