using Business.BlogPostHandlers;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Database.Context;
using Models;
using System.Data.Entity;

namespace UnitTestsProject.BlogPostTesting
{
	public class GetAllBlogPostsQueryHandlerTests
	{
		[Fact]
		public async Task ExecuteAsync_Success()
		{
			var options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "GetAllBlogsDB")
				.Options;
			using (var context = new DBContext(options))
			{
				// Arrange
				var blogPosts = new List<BlogPost>
				{
					new BlogPost { BlogPostId = Guid.NewGuid(), Title = "Test Title 1", Comments = new List<Comment> { new Comment(), new Comment() } },
					new BlogPost { BlogPostId = Guid.NewGuid(), Title = "Test Title 2", Comments = new List<Comment> { new Comment() } }
				};
				await context.BlogPosts.AddRangeAsync(blogPosts);
				await context.SaveChangesAsync();
				var handler = new GetAllBlogPostsQueryHandler(context);
				// Act
				var result = await handler.ExecuteAsync();
				// Assert
				Assert.NotNull(result);
				
				Assert.Equal(blogPosts.Count, result.Count);
				foreach (var blogPost in blogPosts)
				{
					var resultItem = result.Single(x => x.BlogPostId == blogPost.BlogPostId);
					Assert.Equal(blogPost.Title, resultItem.Title);
					Assert.Equal(blogPost.Comments.Count, resultItem.CommentaryCount);
				}
				await context.Database.EnsureDeletedAsync();
			}
		}
	}
}
