using System.ComponentModel.DataAnnotations;

namespace Business.BlogPostHandlers
{
	public class CreateBlogPostCommandHandler
	{
		[MaxLength(200)]
		[Required]
		public string Title { get; set; } = string.Empty;
		[Required]
		public string Content { get; set; } = string.Empty;
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
	}
}
