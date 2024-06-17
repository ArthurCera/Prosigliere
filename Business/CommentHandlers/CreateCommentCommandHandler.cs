using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CommentHandlers
{
	public class CreateCommentCommandHandler
	{
		[Required]
		public string Content { get; set; } = string.Empty;
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
	}
}
