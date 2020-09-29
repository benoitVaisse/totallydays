using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class BedRepository
    {
        private readonly TotallydaysContext _context;

        public BedRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public Bed Create(Bed Bed)
        {
            this._context.Beds.Add(Bed);
            this._context.SaveChanges();
            return Bed;
        }

        public Bed Update(Bed Bed)
        {
            this._context.Beds.Update(Bed);
            this._context.SaveChanges();
            return Bed;
        }

        public async Task<IEnumerable<Bed>> FindAll()
        {
            return await this._context.Beds.ToListAsync();
        }
        public Bed Find(int Id)
        {
            return this._context.Beds.Find(Id);
        }

        public Bed FindOneById(int idBed)
        {
            return this._context.Beds.Find(idBed);
        }

        public Bed FindByName(string Name)
        {
            var bed = from b in this._context.Beds
                      where b.Name == Name
                      select b;
            return bed.FirstOrDefault();
        }

    }
}
