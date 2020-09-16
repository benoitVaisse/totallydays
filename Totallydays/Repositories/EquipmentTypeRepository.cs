using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class EquipmentTypeRepository
    {
        private readonly TotallydaysContext _context;

        public EquipmentTypeRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// retourne tous les types d'quipements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Equipment_type> FindAll()
        {
            return this._context.Equipment_types.ToList();
        }

        /// <summary>
        /// cherche un equiment_type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Equipment_type FindOne(int id)
        {
            Equipment_type EquipmentType = this._context.Equipment_types.Find(id);

            return EquipmentType;
        }

        /// <summary>
        /// create a equipment type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Equipment_type create(Equipment_type model)
        {
            this._context.Equipment_types.Add(model);
            var result = this._context.SaveChanges();
            return model;
        }


        /// <summary>
        /// update a equipment type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Equipment_type Update(Equipment_type model)
        {
            this._context.Equipment_types.Update(model);
            var result = this._context.SaveChanges();
            return model;
        }
    }
}
