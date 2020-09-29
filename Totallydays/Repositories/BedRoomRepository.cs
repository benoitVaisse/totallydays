using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class BedRoomRepository
    {
        private readonly TotallydaysContext _context;

        public BedRoomRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public Bedroom create(Bedroom Bedroom)
        {
            this._context.Bedrooms.Add(Bedroom);
            this._context.SaveChanges();

            return Bedroom;
        }

        public Bedroom Delete(Bedroom b)
        {
            foreach (Bedroom_Bed bb in b.Bedroom_Beds)
            {
                this.DeleteBB(bb);
            }
            this._context.Remove(b);

            return b;
        }

        public Bedroom_Bed DeleteBB(Bedroom_Bed bb)
        {
            this._context.Bedroom_Beds.Remove(bb);
            return bb;
        }

        public void saveChange()
        {
            this._context.SaveChanges();
        }
    }
}
