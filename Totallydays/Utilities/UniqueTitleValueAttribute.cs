using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Utilities
{
    public class UniqueTitleValueAttribute : ValidationAttribute
    {
        private readonly string anotherProperty;

        public UniqueTitleValueAttribute(string anotherProperty)
        {
            this.anotherProperty = anotherProperty;
        }

        /// <summary>
        /// if exist hosting with same Title , set error
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            HostingRepository _hostingRepository = (HostingRepository)ValidationContext.GetService(typeof(HostingRepository));
            var property = ValidationContext.ObjectType.GetProperty(this.anotherProperty);
            var propertyValue = property.GetValue(ValidationContext.ObjectInstance, null).ToString();
            int valueFinal = Int32.Parse(propertyValue);

            var Value = value.ToString();

            Hosting Hosting = _hostingRepository.FindByTitle(Value);

            if (Hosting != null && Hosting.Hosting_id != valueFinal)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    FormatErrorMessage(ValidationContext.DisplayName)
                ));


            }
            return ValidationResult.Success;
        }
    }
}
