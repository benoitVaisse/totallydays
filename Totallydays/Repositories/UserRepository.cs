using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class UserRepository
    {
        private readonly TotallydaysContext _context;

        public UserRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// get all user
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AppUser>> FindAll()
        {
            return await this._context.AppUsers.ToListAsync();
        }

        /// <summary>
        /// get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUser> FindById(int id)
        {

            return await this._context.AppUsers.FindAsync(id);
        }

        /// <summary>
        /// return average of all hosting by user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AppUser> GetBestUser(int limit)
        {

            return this._context.AppUsers.ToList().OrderByDescending(u => u.TotalAverageHosting).Take(limit);

            //var User = (from user in this._context.AppUsers
            //            join hosting in this._context.Hostings on user equals hosting.User
            //            join comment in this._context.Comments on hosting equals comment.Hosting
            //            select user).Take(limit).ToList();

            //List<AppUser> Users = new List<AppUser>();
            //foreach (var item in User)
            //{
            //    Users.Append(item.user);
            //}
            //return User;
            //var toto = this._context.AppUsers.FromSqlRaw("SELECT * FROM dbo.AspNetUsers u " +
            //    "LEFT JOIN dbo.Hostings h on h.userId = u.Id " +
            //    "LEFT JOIN dbo.Comments c on c.hosting_id = h.hosting_id").ToList();
            //return toto;

        }

        public float GetAverageAllHosting(AppUser User)
        {
            try
            {
                var query = (float)(from u in this._context.AppUsers
                             join h in this._context.Hostings on u.Id equals h.UserId
                             join b in this._context.Bookings on h.Hosting_id equals b.HostingHosting_id
                             join c in this._context.Comments on b.Booking_id equals c.BookingBooking_id
                             select c.Rating).Average();
                return query;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        /// <summary>
        /// retourn al liste des utilisateur qui on au moins un hébergement et qui on des reservation en attente
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AppUser>> GetUserWithHosting()
        {
            return await this._context.Users.Where(u => u.Hostings.Count() > 0).ToListAsync();
        }

    }
}
