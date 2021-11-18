using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Validations
{
    public class StartWithUpperCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (null == value || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value.ToString()[0].ToString() != value.ToString()[0].ToString().ToUpper())
            {
                return new ValidationResult("The attribute must be start with Upper letter");
            }

            return ValidationResult.Success;
        }
    }
}
