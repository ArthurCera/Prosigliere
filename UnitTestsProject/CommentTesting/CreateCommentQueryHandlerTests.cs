using Business.CommentHandlers;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Database.Context;
using Models;
using Moq;

namespace UnitTestsProject.CommentTesting
{
	public class CreateCommentQueryHandlerTests
	{
		[Fact]
		public async Task ExecuteAsync_Success()
		{
			// Arrange
			var blogPost = new BlogPost { Title = "Test Title 1", Content = "Blog content" };
			var options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "CreateCommentSuccess")
				.Options;
			using (var context = new DBContext(options))
			{
				await context.AddAsync(blogPost);
				await context.SaveChangesAsync();
				var commentCommand = new CreateCommentCommandHandler
				{
					Content = "Test Comment",
					CreatedDate = DateTime.Now
				};
				var handler = new CreateCommentQueryHandler(context);
				// Act
				var result = await handler.ExecuteAsync(blogPost.BlogPostId,commentCommand);
				// Assert
				Assert.NotNull(result);
				Assert.Equal(blogPost.BlogPostId, result.BlogPostId);
				Assert.Equal(commentCommand.Content, result.Content);
				Assert.Equal(commentCommand.CreatedDate, result.CreatedDate);
				await context.Database.EnsureDeletedAsync();
			}
		}

		[Fact]
		public async Task ExecuteAsync_Exception()
		{
			// Arrange
			var commentCommand = new CreateCommentCommandHandler
			{
				Content = "Test Comment",
				CreatedDate = DateTime.Now
			};
			var options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "CreateCommentException")
				.Options;
			using (var context = new DBContext(options))
			{
				var handler = new CreateCommentQueryHandler(context);
				// Act & Assert
				await Assert.ThrowsAsync<Exception>(() => handler.ExecuteAsync(new Guid(), commentCommand));
				await context.Database.EnsureDeletedAsync();
			}
		}
	}
}
