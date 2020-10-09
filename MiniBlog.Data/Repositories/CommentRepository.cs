using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MiniBlogDBContext db;

        public CommentRepository(MiniBlogDBContext db)
        {
            this.db = db;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            await db.Comments.AddAsync(comment);
            return comment;
        }

        public async Task<int> Commit()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            db.Remove(comment);
            return await Task.FromResult(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsRelatedToBlogPost(Guid blogPostId)
        {
            return await db.Comments.Where(c => c.OwnerPostId == blogPostId).ToListAsync();
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            var entity = db.Comments.Attach(comment);
            entity.State = EntityState.Modified;
            return await Task.FromResult(comment);
        }
    }
}