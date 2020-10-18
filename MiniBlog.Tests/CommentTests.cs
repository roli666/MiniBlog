using FluentAssertions;
using MiniBlog.Core.Entities;
using MiniBlog.Data.InMemory;
using System;
using System.Collections.Generic;
using Xunit;

namespace MiniBlog.Tests
{
    public class CommentTests
    {
        private readonly List<Comment> comments;
        private readonly BlogPostBase blogpost;
        public CommentTests()
        {
            var parentComment = new Comment
            {
                OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                Content = "Ez a messi milyen messire lõtte a labdát exdí",
                Id = Guid.NewGuid(),
            };
            var parentCommentChild = new Comment
            {
                OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                Content = "ja sztem is",
                Id = Guid.NewGuid(),
                Parent = parentComment
            };
            comments = new List<Comment>
            {
                parentComment,
                parentCommentChild,
                 new Comment
                {
                    OwnerPostId = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                    Content="sztem meg nem gyere beszéljük meg",
                    Id=Guid.NewGuid(),
                    Parent = parentCommentChild,
                },
            };
            blogpost = new SportBlogPost
            {
                Content = "Valami sportos vmi",
                CreatedOn = DateTime.Now,
                Id = Guid.Parse(InMemoryBlogPostRepository.SportBlogPostId),
                Title = "Sportos title",
                Comments = comments,
                BackgroundImage = new Image
                {
                    ImagePath = new Uri("img/InMemory/SportBlogPic.jpg", UriKind.Relative)
                }
            }; 
        }

        [Fact]
        public void Comment_count_test()
        {
            var result = blogpost.GetCommentCount(blogpost.Comments);
            result.Should().Be(3);
        }
    }
}
