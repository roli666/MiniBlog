using MiniBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsRelatedToBlogPost(Guid blogPostId);

        Task<Comment> AddComment(Comment comment);

        Task<Comment> UpdateComment(Comment comment);

        Task<Comment> DeleteComment(Comment comment);

        Task<int> Commit();
    }
}