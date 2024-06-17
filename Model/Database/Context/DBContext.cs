using Microsoft.EntityFrameworkCore;
using Models;

namespace Model.Database.Context
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder) //adding EF configurations
		{
			modelBuilder.ApplyConfiguration(new EntityTypeConfiguration.BlogPostConfiguration());
			modelBuilder.ApplyConfiguration(new EntityTypeConfiguration.CommentConfiguration());
		}
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
