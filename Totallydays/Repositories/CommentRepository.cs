using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;

namespace Totallydays.Repositories
{
    public class CommentRepository
    {
        private readonly TotallydaysContext _context;

        public CommentRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        public async Task SaveChange()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task<Comment> FindAsync(int Id)
        {
            return await this._context.Comments.FindAsync(Id);
        }

        public async Task<Comment> CreateAsync(Comment Comment)
        {
            await this._context.Comments.AddAsync(Comment);
            await this.SaveChange();
            return Comment;
        }
    }
}
