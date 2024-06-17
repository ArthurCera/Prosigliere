using Business.BlogPostHandlers;
using Model;
using Model.Database.Context;
using Models;
using System.Diagnostics;

namespace Business.CommentHandlers
{
	public class CreateCommentQueryHandler
	{
		private DBContext _ctx;
		public CreateCommentQueryHandler(DBContext ctx)
		{
			_ctx = ctx;
		}
		public async Task<Comment> ExecuteAsync(Guid blogPostId,CreateCommentCommandHandler commentCommand)
		{
			try
			{
				// Check if the associated BlogPost exists
				var existingBlogPost = await _ctx.BlogPosts.FindAsync(blogPostId);
				if (existingBlogPost == null)
				{
					throw new Exception("BlogPost does not exist.");
				}
				Comment commentary = ConvertToComment(blogPostId,commentCommand);
				//Adding the commentary to the database
				await _ctx.Comments.AddAsync(commentary);
				await _ctx.SaveChangesAsync();
				return commentary;
			}
			catch (Exception ex)
			{//handling the errors
				Debug.WriteLine("error on CreateCommentQueryHandler ExecuteAsync /n");
				Debug.WriteLine(ex.InnerException);
				Debug.WriteLine(ex.StackTrace);
				throw;
			}
		}
		private Comment ConvertToComment(Guid blogPostId, CreateCommentCommandHandler post)
		{
			return new Comment
			{
				BlogPostId = blogPostId,
				Content = post.Content,
				CreatedDate = post.CreatedDate
			};
		}
	}
}
