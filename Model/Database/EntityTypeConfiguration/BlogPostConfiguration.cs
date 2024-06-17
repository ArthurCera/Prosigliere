using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Model.Database.EntityTypeConfiguration
{
	public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
	{
		public void Configure(EntityTypeBuilder<BlogPost> builder)
		{
			builder.ToTable("BlogPost");

			builder.HasKey(s => s.BlogPostId);
			builder.Property(c => c.BlogPostId).HasDefaultValueSql("NEWID()"); //default value is for creating the GUID automatically
			builder.Property(c => c.Title);
			builder.Property(c => c.Content);
			builder.Property(c => c.CreatedDate);
			builder.HasMany(c => c.Comments);
		}
	}
}
