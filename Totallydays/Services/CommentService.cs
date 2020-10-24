using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.ViewsModel;

namespace Totallydays.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(
            CommentRepository commentRepo
        )
        {
            this._commentRepository = commentRepo;
        }

        public async Task<Comment> Create(FormCommentViewModel model, AppUser User)
        {
            Comment Comment = new Comment()
            {
                BookingBooking_id = model.BookingId,
                User_emmiter = User,
                Content = model.Comment,
                Rating = int.Parse(model.Rating),
                Created_at = DateTime.Now

            };

            Comment = await this._commentRepository.CreateAsync(Comment);

            return Comment;
        }
    }
}
