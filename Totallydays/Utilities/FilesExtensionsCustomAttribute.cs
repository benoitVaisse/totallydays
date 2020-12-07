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
    /// <summary>
    /// classe qui va vérifer si l'extension du fichier est conforme aux extensions souhaitées
    /// </summary>
    public class FilesExtensionsCustomAttribute : ValidationAttribute
    {
        private readonly List<string> _fileExtension;

        /// <summary>
        /// construction
        /// </summary>
        /// <param name="fileExtensions"></param>
        public FilesExtensionsCustomAttribute(string fileExtensions)
        {
            // on récupère la chaine de caractere des extension sous forme de tableau
            this._fileExtension = fileExtensions.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        /// <summary>
        /// methode de verification
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ValidationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            //recupération du fichier
            IFormFile File = value as IFormFile;
            if(File != null)
            {
                string ext = Path.GetExtension(File.FileName);
                if (!this._fileExtension.Contains(ext))
                {
                    // si l'extension du fichier n'est pas bonne
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
