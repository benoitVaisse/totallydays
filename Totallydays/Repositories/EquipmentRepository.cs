
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class EquipmentRepository
    {
        private readonly TotallydaysContext _context;

        public EquipmentRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// retourn tous les equipments
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Equipment> FindAll()
        {
            return this._context.Equipments.ToList();
        }

        /// <summary>
        /// retourne un equipement par son id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Equipment FindOneById(int Id)
        {
            return this._context.Equipments.Find(Id);
        }

        /// <summary>
        /// créer un equipement
        /// </summary>
        /// <param name="Equipment"></param>
        /// <returns></returns>
        public Equipment Create(Equipment Equipment)
        {
            this._context.Equipments.Add(Equipment);
            this._context.SaveChanges();
            return Equipment;
        }

        /// <summary>
        /// met a jour un equipement
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        public Equipment Update(Equipment equipment)
        {
            this._context.Equipments.Update(equipment);
            this._context.SaveChanges();
            return equipment;
        }
    }
}
