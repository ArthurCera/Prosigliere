using Model;
using Model.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.BlogPostHandlers
{
	public class GetBlogPostQueryHandler
	{
		private DBContext _ctx;
		public GetBlogPostQueryHandler(DBContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<BlogPost> ExecuteAsync(Guid BlogPostId)
		{
			try
			{
				//returning a specific blog post that we got from the database based on the ID
				return await _ctx.BlogPosts.Include(x => x.Comments).Where(x => x.BlogPostId.Equals(BlogPostId)).FirstOrDefaultAsync();
			}
			catch (Exception ex) //handling the errors
			{
				Debug.WriteLine("Error on GetBlogPostQueryHandler ExecuteAsync /n");
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				throw; //rethrowing the exception while preserving the original data and stack trace
			}
		}
	}
}
