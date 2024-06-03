using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.Data.Models;

namespace ProstoAndVkusno.DBContext
{
    public class ApplicationContext: DbContext
	{
		public DbSet<Users> _users {  get; set; }

		public DbSet<Product> _products { get; set; }
		public DbSet<Category> _categories { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
		{
			Database.EnsureCreated();
		}
	}
}
