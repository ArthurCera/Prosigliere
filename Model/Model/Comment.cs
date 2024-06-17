using Model;
using System.ComponentModel.DataAnnotations;
namespace Models
{
	public class Comment //Class to hold the commentaries made on a blog post
	{
		[Key]
		public Guid CommentId { get; set; } = Guid.Empty; //unique identifier
		[Required]
		//public Guid UserId { get; set; } //Id of the user who made the commentary
		public Guid BlogPostId { get; set; } //Id of the post in which the commentary was made
		[Required]
		public string Content { get; set; } = string.Empty; //Content of the commentary (DB NVarchar(max)) -> Max size on the db
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow; //date and time of the creation of the time, using UTC timezone	
	}
}