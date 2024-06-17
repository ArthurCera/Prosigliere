using Business.BlogPostHandlers;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Database.Context;
using Models;

namespace UnitTestsProject.BlogPostTesting
{
	public class GetBlogPostQueryHandlerTests
	{
		[Fact]
		public async Task ExecuteAsync_Success()
		{
			// Arrange
			var postId = Guid.NewGuid();
			var comments = new[]
			{
				new Comment { Content = "Comment 1" },
				new Comment { Content = "Comment 2" }
			};
			var expectedBlogPost = new BlogPost
			{
				BlogPostId = postId,
				Title = "Test Post",
				Comments = comments.ToList()
			};
			var options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "GetBlogPostSucess")
				.Options;
			using (var context = new DBContext(options))
			{
				//adding the blog post on the db
				await context.BlogPosts.AddAsync(expectedBlogPost);
				await context.SaveChangesAsync();
				var handler = new GetBlogPostQueryHandler(context);
				// Act
				var result = await handler.ExecuteAsync(postId);
				// Assert
				Assert.NotNull(result);
				Assert.Equal(expectedBlogPost.BlogPostId, result.BlogPostId);
				Assert.Equal(expectedBlogPost.Title, result.Title);
				Assert.Equal(expectedBlogPost.Comments.Count, result.Comments.Count);
				Assert.All(expectedBlogPost.Comments, comment => Assert.Contains(result.Comments, c => c.Content == comment.Content));
				await context.Database.EnsureDeletedAsync();
			}
		}
		[Fact]
		public async Task ExecuteAsync_NotFound()
		{
			// Arrange
			var postId = Guid.NewGuid();
			var options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "GetBlogPostNotFound")
				.Options;
			using (var context = new DBContext(options))
			{
				var handler = new GetBlogPostQueryHandler(context);
				// Act
				var result = await handler.ExecuteAsync(postId);
				// Assert
				Assert.Null(result);
				await context.Database.EnsureDeletedAsync();
			}
		}
	}
}
