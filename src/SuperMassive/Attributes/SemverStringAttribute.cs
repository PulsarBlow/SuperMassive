namespace SuperMassive
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// A validation attribute which will ensure that the provided string is a valid semver format
    /// http://semver.org/
    /// </summary>
    public class SemverStringAttribute : ValidationAttribute
    {
        public SemverStringAttribute()
            : base(Properties.Resources.Validation_NotValidSemverString)
        { }

        /// <summary>
        /// Validate the given value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (!RegexHelper.IsSemver(Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty))
            {
                return new ValidationResult(
                    validationContext != null ?
                        FormatErrorMessage(validationContext.DisplayName) :
                        ErrorMessage
                );
            }

            return null;
        }
    }
}
