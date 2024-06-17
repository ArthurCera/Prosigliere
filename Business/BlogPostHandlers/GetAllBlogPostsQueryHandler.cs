using Model.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.BlogPostHandlers
{
    public class GetAllBlogPostsQueryHandler
    {
        private DBContext _ctx;
        public GetAllBlogPostsQueryHandler(DBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<List<ReturnModel>> ExecuteAsync()
        {
            try
            {
                //returning a list with the "return model" that contains the id, title and comment count for all the blog posts
                return await _ctx.BlogPosts.Include(x => x.Comments)
                    .Select(x => new ReturnModel
					{
                        BlogPostId = x.BlogPostId,
                        Title = x.Title,
                        CommentaryCount = x.Comments.Count
                    }).ToListAsync();
			}
            catch(Exception ex) //handling errors
            {
                Debug.WriteLine("Error on GetAllBlogPostsQueryHandler ExecuteAsync /n");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                throw; //rethrowing the exception while preserving the original data and stack trace
			}
		}

		public class ReturnModel // Having this model to explicitly have a return type for this handler
        {
            public Guid BlogPostId { get; set; }
            public string Title { get; set; }
            public int CommentaryCount { get; set;}
        }
    }
}
