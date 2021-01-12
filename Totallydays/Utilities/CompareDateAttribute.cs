using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Utilities
{
    public class CompareDateAttribute : ValidationAttribute
    {
        private readonly string anotherProperty;
        private readonly string _compare;

        public CompareDateAttribute(string anotherProperty, string compare)
        {
            this.anotherProperty = anotherProperty;
            this._compare = compare;
        }

        /// <summary>
        /// compare 2 date
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            var currentValue = (DateTime)value;

            var property = ValidationContext.ObjectType.GetProperty(this.anotherProperty);
            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var OtherValue = (DateTime)property.GetValue(ValidationContext.ObjectInstance, null);
            bool result;

            switch(this._compare){

                case "<":
                    result = currentValue < OtherValue;
                    break;
                case "<=":
                    result = currentValue <= OtherValue;
                    break;
                case ">":
                    result = currentValue > OtherValue;
                    break;
                case ">=":
                    result = currentValue >= OtherValue;
                    break;
                case "==":
                default:
                    result = currentValue == OtherValue;
                    break;

            }

            if (!result)
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
