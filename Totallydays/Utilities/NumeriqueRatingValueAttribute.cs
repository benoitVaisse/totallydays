using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Totallydays.Utilities
{
    public class NumeriqueRatingValueAttribute : ValidationAttribute
    {

        public NumeriqueRatingValueAttribute()
        {
        }

        /// <summary>
        /// look if rating is a number range 0,5
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            string patern = "[0-5]";
            Match Match = Regex.Match(value.ToString(), patern);

            if (Match.Success)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format(
                CultureInfo.CurrentCulture,
                FormatErrorMessage(ValidationContext.DisplayName)
            ));
        }
    }
}
