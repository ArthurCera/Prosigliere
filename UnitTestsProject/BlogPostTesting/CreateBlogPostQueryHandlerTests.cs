using Xunit;
using Moq;
using Business.BlogPostHandlers;
using Model.Database.Context;
using Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
namespace UnitTestsProject.BlogPostTesting
{
	public class CreateBlogPostQueryHandlerTests
	{
		[Fact]
		public async Task ExecuteAsync_Success()
		{
			// Arrange
			var post = new CreateBlogPostCommandHandler
			{
				Title = "Test Title",
				Content = "Test Content",
				CreatedDate = DateTime.Now
			};
			DbContextOptions<DBContext>  _options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "CreateBlogPostSuccess")
				.Options;
			using (var context = new DBContext(_options))
			{
				var handler = new CreateBlogPostQueryHandler(context);
				// Act
				var result = await handler.ExecuteAsync(post);
				// Assert
				Assert.NotNull(result);
				Assert.Equal(post.Title, result.Title);
				Assert.Equal(post.Content, result.Content);
				Assert.Equal(post.CreatedDate, result.CreatedDate);
				await context.Database.EnsureDeletedAsync();
			}
		}
		[Fact]
		public async Task ExecuteAsync_Exception()
		{
			// Arrange
			var post = new CreateBlogPostCommandHandler
			{
				Title = "Test Title",
				Content = null,
				CreatedDate = DateTime.Now
			};
			DbContextOptions<DBContext> _options = new DbContextOptionsBuilder<DBContext>()
				.UseInMemoryDatabase(databaseName: "CreateBlogPostException")
				.Options;
			using (var context = new DBContext(_options))
			{
				var handler = new CreateBlogPostQueryHandler(context);
				// Act & Assert
				await Assert.ThrowsAsync<DbUpdateException>(() => handler.ExecuteAsync(post));
				await context.Database.EnsureDeletedAsync();
			}
		}
	}
}
