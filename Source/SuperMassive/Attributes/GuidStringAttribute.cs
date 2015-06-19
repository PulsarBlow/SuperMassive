using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SuperMassive
{
    /// <summary>
    /// A validation attribute which will ensure that the provided guid string is a valid Guid format
    /// </summary>
    public class GuidStringAttribute : ValidationAttribute
    {
        private bool _allowEmpty = false;

        /// <summary>
        /// Creates a new instance of the <see cref="GuidStringAttribute"/>
        /// </summary>
        public GuidStringAttribute()
            : this(true)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="GuidStringAttribute"/>
        /// </summary>
        /// <param name="allowEmpty">True if Guid.Empty is not a valid value</param>
        public GuidStringAttribute(bool allowEmpty)
            : base(Properties.Resources.Validation_NotValidGuidString)
        {
            _allowEmpty = allowEmpty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string input = Convert.ToString(value, CultureInfo.InvariantCulture);

            Guid result;
            if (!Guid.TryParse(input, out result))
            {
                return new ValidationResult(
                    validationContext != null ?
                        FormatErrorMessage(validationContext.DisplayName) :
                        ErrorMessage);
            }
            if (result == Guid.Empty && !_allowEmpty)
            {
                return new ValidationResult(
                    validationContext != null ?
                        FormatErrorMessage(validationContext.DisplayName) :
                        ErrorMessage);
            }

            return null;
        }
    }
}
