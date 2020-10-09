using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Data.InMemory
{
    public class InMemoryCommentRepository : ICommentRepository
    {
        private readonly List<Comment> comments;

        public InMemoryCommentRepository()
        {
            var parentComment = new Comment
            {
                OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                Content = "Ez a messi milyen messire lőtte a labdát exdí",
                Id = Guid.NewGuid(),
            };
            comments = new List<Comment>
            {
                parentComment,
                new Comment
                {
                    OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                    Content="ja sztem is",
                    Id=Guid.NewGuid(),
                    Parent = parentComment
                },
                 new Comment
                {
                    OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                    Content="sztem meg nem gyere beszéljük meg",
                    Id=Guid.NewGuid(),
                },
            };
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            comments.Add(comment);
            return await Task.FromResult(comment);
        }

        public async Task<int> Commit()
        {
            return await Task.FromResult(1);
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            comments.Remove(comment);
            return await Task.FromResult(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsRelatedToBlogPost(Guid blogPostId)
        {
            return await Task.FromResult(comments.Where(c => c.OwnerPostId == blogPostId));
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            comments[comments.FindIndex(ind => ind.Equals(comment))] = comment;
            return await Task.FromResult(comment);
        }
    }
}