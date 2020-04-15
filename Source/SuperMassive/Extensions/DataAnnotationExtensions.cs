#nullable enable

namespace SuperMassive.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Extension methods for data annotation types
    /// </summary>
    public static class DataAnnotationExtensions
    {
        /// <summary>
        /// Converts an <see cref="IEnumerable{ValidationResult}"/> to its <see cref="Dictionary{TKey, TValue}"/> counterpart.
        /// </summary>
        /// <param name="extended"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this IEnumerable<ValidationResult> extended)
        {
            Guard.ArgumentNotNull(extended, nameof(extended));

            return extended.ToDictionary(x => string.Join(",", x.MemberNames), x => x.ErrorMessage);
        }
    }
}
