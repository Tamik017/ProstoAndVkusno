using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.Models;

namespace ProstoAndVkusno.DBContext
{
	public class ApplicationContext: DbContext
	{
		public DbSet<Users> _users {  get; set; }


		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
		{
			Database.EnsureCreated();
		}
	}
}
