using System;

#nullable enable

namespace SuperMassive
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Extensions;

    /// <summary>
    /// A collection wrapper which is used by the <see cref="ValidationFailedException"/>.
    /// </summary>
    public class ValidationResultCollection : List<ValidationResult>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ValidationResultCollection"/> class.
        /// </summary>
        /// <param name="capacity"></param>
        public ValidationResultCollection(int capacity)
            : base(capacity)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ValidationResultCollection"/> class.
        /// </summary>
        public ValidationResultCollection()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ValidationResultCollection"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public ValidationResultCollection(IEnumerable<ValidationResult> collection)
            : base(collection)
        { }

        /// <summary>
        /// Returns the string representation of the inner list of <see cref="ValidationResult"/>
        /// or the representation of the type if the inner list is empty.
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            if (Count == 0)
            {
                return base.ToString();
            }

            var builder = new StringBuilder();
            foreach (var result in this)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0} ({1})\n",
                    result.ErrorMessage,
                    result.MemberNames != null ? result.MemberNames.Join(", ") : string.Empty);
            }

            return builder.ToString();
        }
    }
}
