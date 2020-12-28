using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Totallydays.ViewsModel;

namespace Totallydays.Services
{
    /// <summary>
    /// service captcha
    /// </summary>
    public class ReCaptchaService
    {
        private ReCaptchaSetting _setting;
        public ReCaptchaService(IOptions<ReCaptchaSetting> settings)
        {
            this._setting = settings.Value;
        }

        /// <summary>
        /// fonction qui verifie si le captcha a bien été envoyé et si il est valide ou pas
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<bool> VerifyResponse(string token)
        {
            var client = new HttpClient();
            GoogleReCaptchaData googleData = new GoogleReCaptchaData()
            {
                response = token,
                secret = this._setting.ReCAPTCHA_Secret_key
            };

            var url = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={googleData.secret}&response={googleData.response}");
            var response = JsonConvert.DeserializeObject<GoogleResponse>(url);

            return response.success;

        }
    }

    public class GoogleReCaptchaData
    {
        public string response { get; set; }

        public string secret { get; set; }

    }

    public class GoogleResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }// timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
        public string hostname { get; set; } // the hostname of the site where the reCAPTCHA was solved
    }


}
