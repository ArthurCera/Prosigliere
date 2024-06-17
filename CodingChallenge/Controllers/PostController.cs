using Business.BlogPostHandlers;
using Business.CommentHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using Models;
using System.Diagnostics;

namespace CodingChallenge.Controllers
{
	[ApiController]
	[Route("api")]
	public class PostController : ControllerBase
	{
		/// <summary>
		/// Retrieves all the Titles, ID's and commentary count for the posts.
		/// </summary>
		/// <returns>
		/// [
		///		{
		///			Id	  : b0504cb3-0749-41ea-a6bd-a419158f269b, //Guid of the Blog Post
		///			Title : "Sample Title",
		///			CommentaryCount : 1
		///		},
		///		{
		///			Id	  : 493dc2de-1b9a-4f8a-a549-73bbe9a48d04, //Guid of the Blog Post
		///			Title : "Sample Title 2",
		///			CommentaryCount : 2
		///		}
		/// ]
		/// </returns>
		/// <response code="200">List of blog posts with the commentary count.</response>
		/// <response code="400">Error message with the bad request.</response>
		[HttpGet("posts")]
		public async Task<IActionResult> GetPosts([FromServices] GetAllBlogPostsQueryHandler handler){
			try
			{
				return Ok(await handler.ExecuteAsync()); //Execution and return of the function that gets the blog posts
			}
			catch (Exception ex)//handling exceptions
			{
				Debug.WriteLine("Error on PostController HttpGet/posts /n");
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				return BadRequest(ex.Message); //return bad request with specific error message
			}
		}
		/// <summary>
		/// Creates a new blog post.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///     {
		///        "title": "Sample test blog Title",
		///        "content": "Sample Content."
		///     }
		/// </remarks>
		/// <param name="blogPost">The post that is going to be created.</param>
		/// <returns> The created blog post or error message.</returns>
		/// <response code="200">Blog post was created correctly.</response>
		/// <response code="400">Blog post was not created properly.</response> 
		[HttpPost("posts")]
		public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostCommandHandler blogPost,[FromServices] CreateBlogPostQueryHandler handler){
			try
			{
				return Ok(await handler.ExecuteAsync(blogPost)); //Execution and return of the function that Creates the blog posts
			}catch (Exception ex)//handling exceptions
			{
				Debug.WriteLine("Error on PostController HttpGet/posts /n");
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				return BadRequest(ex.Message); //return bad request with specific error message
			}
		}
		/// <summary>
		/// Retrieve the blog post with the given Guid.
		/// </summary>
		/// <remarks>
		/// <param name="id">The Guid of the blog post to be retrieved.</param>
		/// <returns>{
		///		BlogPostId : 6af5a4fa-5c72-45d2-b61a-c37df60c8617, //Guid of the Blog Post
		///		Title : "Blog Title",
		///		Content : "Sample Content",
		///		CreatedDate : 2010 23:59:59.999999 -06:00 // DateTimeOffset,
		///		Comments : {
		///			CommentId : "Comment Id",
		///			Content   : "Commentary content",
		///			CreatedDate : 2010 23:59:59.999999 -06:00 // DateTimeOffset	
		///		}
		/// }
		/// </returns>
		/// <response code="200">Blog Post Returned.</response>
		/// <response code="400">Error Message.</response>
		[HttpGet("posts{id}")]
		public async Task<IActionResult> GetPosts(Guid id, [FromServices] GetBlogPostQueryHandler handler)
		{
			try
			{
				if (id == Guid.Empty) return BadRequest("Blog post id is null;"); //verifying if the ID is not null
				return Ok(await handler.ExecuteAsync(id)); //Execution and return of the function that Creates the blog posts
			}
			catch (Exception ex)//handling exceptions
			{
				Debug.WriteLine("Error on PostController HttpGet/posts /n");
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				return BadRequest(ex.Message); //return bad request with specific error message
			}
		}
		/// <summary>
		/// Adds a commentary to the blog post.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /BlogPosts/posts{id}/comments
		///     {
		///			Content   : "Commentary content"
		///     }
		///
		/// </remarks>
		/// <param name="id">Guid of the post that the commentary is going to be linked to.</param>
		/// <param name="commentary">Commentary that is going to be added.</param>
		/// <returns>
		///     {
		///			CommentId : 07b2b6f0-4b72-45b7-bc61-9d3f7c5bb6c7, //Guid
		///			BlogPostId: b31e2faa-2c93-43ee-8cb6-f985b9e01724, //Guid of the linked blog post
		///			Content   : "Commentary content",
		///			CreatedDate : 2010 23:59:59.999999 -06:00 // DateTimeOffset	
		///     }
		///     </returns>
		/// <response code="200">Returns the created comment</response>
		/// <response code="400">Error message.</response>
		[HttpPost("posts{id}/comments")]
		public async Task<IActionResult> AddCommentToPost(Guid id, [FromBody] CreateCommentCommandHandler commentary, [FromServices] CreateCommentQueryHandler handler)
		{
			try
			{
				if (id == Guid.Empty) return BadRequest("Blog post id is null;"); //verifying if the ID is not null
				return Ok(await handler.ExecuteAsync(id,commentary)); //Execution and return of the function that Creates the blog posts
			}
			catch (Exception ex)//handling exceptions
			{
				Debug.WriteLine("Error on PostController HttpGet/posts /n");
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
				return BadRequest(ex.Message); //return bad request with specific error message
			}
		}
	}
}
