using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Services
{
    public class GoogleGeoCodeResponse
    {
        public string status { get; set; }
        public results[] results { get; set; }
    }

    public class results
    {
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string[] types { get; set; }
        public address_component[] address_components { get; set; }
    }

    public class geometry
    {
        public string location_type { get; set; }
        public location location { get; set; }
    }

    public class location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class address_component
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }

    public class GoogleMapService
    {
        private IConfiguration _config;
        private readonly HostingRepository _hostingRepository;

        public GoogleMapService(IConfiguration configuration, HostingRepository hostingRepo)
        {
            this._config = configuration;
            this._hostingRepository = hostingRepo;
        }

        /// <summary>
        /// rentre en base de donnée la latitude et longitude de l'hébergement
        /// </summary>
        /// <param name="Hosting"></param>
        /// <returns></returns>
        public Hosting setLngLgt(Hosting Hosting)
        {
            string adress = Hosting.Address + '+' + Hosting.Post_code + '+' + Hosting.City;
            var adressJson = "https://maps.googleapis.com/maps/api/geocode/json?address=" + adress + "&key=" + this._config["apis:google:google_map"];
            GoogleGeoCodeResponse result = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(new WebClient().DownloadString(adressJson));
            if(result.status == "OK")
            {
                Hosting.Lat = result.results[0].geometry.location.lat;
                Hosting.Lng = result.results[0].geometry.location.lng;

                this._hostingRepository.Update(Hosting);
            }
            return Hosting;
        }
    }
}
