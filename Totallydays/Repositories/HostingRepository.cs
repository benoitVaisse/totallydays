using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class HostingRepository
    {
        private readonly TotallydaysContext _context;

        public HostingRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// trouve un hébergement par son ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Hosting Find(int Id)
        {
            return this._context.Hostings.Find(Id);
        }

        /// <summary>
        /// retourne tous les hébergements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hosting> FindAll()
        {
            return this._context.Hostings.ToList();
        }

        /// <summary>
        /// selectionne les hosting d'un utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Hosting>> FindByUser(AppUser user)
        {
            var query = await this._context.Hostings.Where(h => h.User == user).ToListAsync();
            return query;
        }

        /// <summary>
        /// search hosting by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Hosting FindByTitle(string title)
        {
            return this._context.Hostings.Where(h => h.Title == title).FirstOrDefault();
        }

        /// <summary>
        /// get hosting by slug
        /// </summary>
        /// <param name="Slug"></param>
        /// <returns></returns>
        public Hosting FindBySlug(string Slug)
        {
            return this._context.Hostings.Where(h => h.Slug == Slug).FirstOrDefault();
        }

        /// <summary>
        /// creer un hebergement
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public Hosting Create(Hosting hosting)
        {
            this._context.Hostings.Add(hosting);
            this._context.SaveChanges();
            return hosting;
        }

        /// <summary>
        /// met a jour un hebergemment
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public Hosting Update(Hosting hosting)
        {
            this._context.Update(hosting);
            this._context.SaveChanges();
            return hosting;
        }


        /// <summary>
        /// rajoute une ligne dans la table hosting_equipment
        /// </summary>
        /// <param name="HE"></param>
        /// <returns></returns>
        public Hosting_Equipment CreateHostingEquipment(Hosting_Equipment HE)
        {
            this._context.Hosting_Equipment.Add(HE);
            this._context.SaveChanges();
            return HE;
        }

        /// <summary>
        /// supprime de la table les hosting_equipment by hosting
        /// </summary>
        /// <param name="Hosting"></param>
        /// <returns></returns>
        public Hosting DeleteHostingEquipmentByHosting(Hosting Hosting)
        {
            foreach (Hosting_Equipment HE in Hosting.Hosting_Equipment)
            {
                this._context.Hosting_Equipment.Remove(HE);
            }
            this._context.SaveChanges();
            return Hosting;
        }
    }
}
