using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Utilities
{
    public class DateLessGreaterThanAttribute : ValidationAttribute
    {
        private readonly DateTime _date;
        private readonly string compare;

        /// <summary>
        /// compare uen date avec une autre date specialfier , si la date n'est pas specifié , on prendra la date d'aujourdhui
        /// element de comparaison (< , <=, >, >=)
        /// </summary>
        /// <param name="compare"></param>
        /// <param name="date"></param>
        public DateLessGreaterThanAttribute(string compare = ">", string date = null)
        {
            this.compare = compare;
            if(date== null)
            {
                this._date = DateTime.Now;
            }
            else
            {
                this._date = DateTime.Parse(date);
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            DateTime Value = (DateTime)value;
            bool result;

            switch (this.compare)
            {
                case "<=":
                    result = Value <= this._date;
                    break;
                case "<":
                    result = Value < this._date;
                    break;
                case ">=":
                    result = Value >= this._date;
                    break;
                case ">":
                default:
                    result = Value > this._date;
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
