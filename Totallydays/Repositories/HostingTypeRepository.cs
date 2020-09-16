using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class HostingTypeRepository
    {
        private readonly TotallydaysContext _context;

        public HostingTypeRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public Hosting_type Find(int Id)
        {
            return this._context.Hosting_Types.Find(Id);
        }

        public async Task<IEnumerable<Hosting_type>> FindAll()
        {
            return await this._context.Hosting_Types.ToListAsync();
        }

        public Hosting_type Create(Hosting_type ht)
        {
            this._context.Hosting_Types.Add(ht);
            this._context.SaveChanges();
            return ht;
        }

        public Hosting_type Update(Hosting_type ht)
        {
            this._context.Hosting_Types.Update(ht);
            this._context.SaveChanges();
            return ht;
        }
    }
}
