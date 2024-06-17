using Model;
using Model.Database.Context;
using System.Diagnostics;

namespace Business.BlogPostHandlers
{
	public class CreateBlogPostQueryHandler
	{
		private DBContext _ctx;
		public CreateBlogPostQueryHandler(DBContext ctx)
		{
			_ctx = ctx;
		}
		public async Task<BlogPost> ExecuteAsync(CreateBlogPostCommandHandler post) //adding the blog post on the database
		{
			try
			{
				BlogPost blogPost = ConvertToBlogPost(post);
				await _ctx.AddAsync(blogPost);
				await _ctx.SaveChangesAsync();
				return blogPost;
			}
			catch (Exception ex) //writing the logs and returning the error message
			{
				Debug.WriteLine("error on CreateBlogPostQueryHandler ExecuteAsync /n");
				Debug.WriteLine(ex.InnerException);
				Debug.WriteLine(ex.StackTrace);
				throw; //rethrowing the exception while preserving the original data and stack trace
			}
		}
		private BlogPost ConvertToBlogPost(CreateBlogPostCommandHandler post)
		{
			return new BlogPost
			{
				Title = post.Title,
				Content = post.Content,
				CreatedDate = post.CreatedDate
			};
		}
	}
}
