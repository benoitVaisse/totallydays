using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminBedController : AdminController
    {
        private readonly BedRepository _bedrepository;
        private readonly HostingTypeRepository _hostingType;

        public AdminBedController(BedRepository BedRepo, HostingTypeRepository HostingTypeRepository)
        {
            this._bedrepository = BedRepo;
            this._hostingType = HostingTypeRepository;
        }

        [HttpGet("CREATEBED", Name = "account_create_bed")]
        public IActionResult createBed()
        {
            Bed Simple = new Bed()
            {
                Name = "simple",
                Image = "simple.png"
            };
            Bed Double = new Bed()
            {
                Name = "double",
                Image = "double.png"
            };
            List<Bed> liste = new List<Bed> {
                Simple,
                Double
            };

            foreach (Bed bed in liste)
            {
                var exist = this._bedrepository.FindByName(bed.Name);
                Bed newBed = exist != null ? this._bedrepository.Update(exist) : this._bedrepository.Create(bed);
            }


            return Json(new { liste });
        }


        [HttpGet("CREATEHOSTINGTYPE", Name = "account_create_hosting_type")]
        public IActionResult createHostingType()
        {
            Hosting_type Logement = new Hosting_type()
            {
                Hosting_type_id = 1,
                Name = "Logement entier",
            };
            Hosting_type Chambre = new Hosting_type()
            {
                Hosting_type_id = 2,
                Name = "Chambre privée",
            };
            Hosting_type ChambreHotel = new Hosting_type()
            {
                Hosting_type_id = 3,
                Name = "Chambre d'hôtel",
            };
            Hosting_type ChambrePartage = new Hosting_type()
            {
                Hosting_type_id = 4,
                Name = "Chambre partagée",
            };
            List<Hosting_type> liste = new List<Hosting_type> {
                Logement,
                Chambre,
                ChambreHotel,
                ChambrePartage,
            };

            foreach (Hosting_type ht in liste)
            {
                var exist = this._hostingType.Find(ht.Hosting_type_id);
                if(exist == null)
                {
                    ht.Hosting_type_id = 0;
                    this._hostingType.Create(ht);
                }
                else
                {
                    this._hostingType.Update(ht);
                }

                this._hostingType.Update(ht);
            }


            return Json(new { liste });
        }
    }
}