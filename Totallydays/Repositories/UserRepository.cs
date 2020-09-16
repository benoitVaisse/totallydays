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

        ///// <summary>
        ///// return average of all hosting by user
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<AppUser> GetTotalRatingHosting()
        //{
        //    //var rating = from user in this._context.AppUsers
        //    //             join hosting in this._context.Hostings on user equals hosting.User
        //    //             join comment in this._context.Comments on hosting equals comment.Hosting
        //    //             group new { user, comment } by new { user.Id, comment.Comment_id } into u
        //    //             select new { user = u, average = u.Average(c => c.comment.Rating) };

        //    //var result = rating.AsEnumerable().Select(u => new { user = u.user, average = u.average }).ToList();
        //    //return result;


        //    var User = this._context.Comments.Where(c => c.User_receiver == User).GroupBy(c => c.User_receiver)
        //                .Select(c => new { average = c.Average(t => t.Rating) });

        //    var Average = rating.Select(r => r.average).First();
        //    return (float)Average;

        //    return query;


        //}

    }
}
