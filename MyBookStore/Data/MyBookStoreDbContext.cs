using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBookStore.Common;
using MyBookStore.Models;

namespace MyBookStore.Data
{
	public class MyBookStoreDbContext 
		: IdentityDbContext<ApplicationUser>
	{
		public MyBookStoreDbContext()
		{

		}

		public MyBookStoreDbContext(DbContextOptions<MyBookStoreDbContext> options)
			: base(options)
		{

		}

		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<SubGenre> SubGenres { get; set; }

		public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Forum> Forums { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ApplicationUserLibrary> ApplicationUserLibraries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(DbConfiguration.ConnectionString);
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MyBookStoreDbContext).Assembly);
		}
	}
}
