using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Utilities
{
    public class FilesExtensionsCustomAttribute : ValidationAttribute
    {
        private readonly List<string> _fileExtension;
        public FilesExtensionsCustomAttribute(string fileExtensions)
        {
            this._fileExtension = fileExtensions.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            IFormFile File = value as IFormFile;
            if(File != null)
            {
                string ext = Path.GetExtension(File.FileName);
                if (!this._fileExtension.Contains(ext))
                {
                    return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    FormatErrorMessage(ValidationContext.DisplayName)
                ));
                }
            }


            return ValidationResult.Success;
        }
    }
}
