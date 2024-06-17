using Models;
using System.ComponentModel.DataAnnotations;
namespace Model
{
	public class BlogPost // Data model for the BlogPost class, this class is for the posts of the users
	{
		[Key]
		public Guid BlogPostId { get; set; } = Guid.Empty; // Id of the blog post (unique identifier)
		[MaxLength(200)] // setting the max lenght of the title -> 200 characters
		[Required]
		public string Title { get; set; } = string.Empty; // String (DB NVarchar(200)) that contains the title of the post
		[Required]
		public string Content { get; set; } = string.Empty; // String (DB NVarchar(max)) content of the post
		//public Guid UserId { get; set; } // Guid of the user that created the post
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow; //date and time of the creation of the time, using UTC timezone
		public List<Comment>? Comments { get; set; } = new List<Comment>();
	}
}
