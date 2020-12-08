using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class ImageRepository
    {
        private TotallydaysContext _context;

        public ImageRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// trouve une image par id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Image> Find(int id)
        {
            return await this._context.Images.FindAsync(id);
        }

        /// <summary>
        /// trouve les images d'un hébergement
        /// </summary>
        /// <param name="Hosting"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Image>> FindImageToHosting(Hosting Hosting)
        {
            var query = this._context.Images.Where(i => i.Hosting == Hosting);
            return await query.ToListAsync();
        }

        /// <summary>
        /// ajout d'une image
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public Image Create(Image Image)
        {
            this._context.Images.Add(Image);
            this._context.SaveChanges();
            return Image;
        }

        /// <summary>
        /// supression d'une image
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public Image Delete(Image Image)
        {
            this._context.Images.Remove(Image);
            this._context.SaveChanges();
            return Image;
        }
    }
}
