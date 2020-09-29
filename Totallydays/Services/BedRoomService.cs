using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Services
{
    public class BedRoomService
    {
        private readonly BedRoomRepository _bedroomrepo;
        private readonly BedRepository _bedrepo;

        public BedRoomService(BedRoomRepository bedroomRepo, BedRepository bedRepo)
        {
            this._bedroomrepo = bedroomRepo;
            this._bedrepo = bedRepo;
        }


        public void RemoveBed(IEnumerable<Bedroom> Bedrooms)
        {
            foreach (Bedroom b in Bedrooms)
            {
                this._bedroomrepo.Delete(b);
            }

            this._bedroomrepo.saveChange();
        }

        public IEnumerable<Bedroom> CreateBedRoom(IEnumerable<Bedroom> Bedrooms, Hosting Hosting, ref List<string> erroMessage, ref List<string> successMessage)
        {
            List<Bedroom> NewBedRoomList = new List<Bedroom>();
            int iteration = 1;
            bool valid = true;
            foreach (Bedroom b in Bedrooms)
            {
                valid = true;
                foreach (Bedroom_Bed bb in b.Bedroom_Beds)
                {
                    Bed Bed = this._bedrepo.FindOneById(bb.BedBed_id);
                    if(Bed == null)
                    {
                        valid = false;
                    }

                }

                if(valid == true)
                {
                    b.Name = "Chambre " + iteration;
                    b.Hosting = Hosting;
                    NewBedRoomList.Add(this._bedroomrepo.create(b));
                    successMessage.Add($"La {b.Name} a bien été ajoutée.");
                }
                else
                {
                    erroMessage.Add($"Erreur lors de l'ajout de la chambre {iteration}");
                }
                iteration++;
            }

            return NewBedRoomList;
        }
    }
}
