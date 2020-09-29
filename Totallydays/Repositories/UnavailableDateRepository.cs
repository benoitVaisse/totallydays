using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class UnavailableDateRepository
    {
        private readonly TotallydaysContext _context;

        public UnavailableDateRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Unavailable_date>> FindAll()
        {
            return await this._context.Unavailable_dates.ToListAsync();
        }

        public async Task<IEnumerable<Unavailable_date>> FindByHosting(int hosting_id)
        {
            return await this._context.Unavailable_dates.Where(u => u.HostingHosting_id == hosting_id).ToListAsync();
        }

        public async Task<Unavailable_date> Create(Unavailable_date date)
        {
            await this._context.Unavailable_dates.AddAsync(date);
            await this._context.SaveChangesAsync();

            return date;
        }

        public async Task<Unavailable_date> Update(Unavailable_date date)
        {
            this._context.Unavailable_dates.Update(date);
            await this._context.SaveChangesAsync();

            return date;
        }

        public async Task<Unavailable_date> Delete(Unavailable_date date)
        {
            this._context.Unavailable_dates.Remove(date);
            await this._context.SaveChangesAsync();

            return date;
        }

    }
}
