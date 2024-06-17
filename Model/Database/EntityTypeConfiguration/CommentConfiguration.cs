using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace Model.Database.EntityTypeConfiguration
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comment");
			builder.HasKey(s => s.CommentId);
			builder.Property(c => c.CommentId).HasDefaultValueSql("NEWID()");
			builder.Property(c => c.BlogPostId);
			builder.Property(c => c.Content);
			builder.Property(c => c.CreatedDate);
		}
	}
}
