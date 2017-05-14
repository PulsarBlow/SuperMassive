

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
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null)
                return null;
            return validationResults.ToDictionary(x => String.Join(",", x.MemberNames), x => x.ErrorMessage);
        }
    }
}
