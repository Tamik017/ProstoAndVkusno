using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.mocks;
using ProstoAndVkusno.Data.Repository;
using ProstoAndVkusno.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Login";
	});

builder.Services.AddAuthorization();
builder.Services.AddMvc();
builder.Services.AddTransient<IAllProducts, ProductRepository>();
builder.Services.AddTransient<IProductCategory, CategoryRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}"
	);

using (var scope = app.Services.CreateScope())
{
    ApplicationContext content = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
	DBObject.Initial(content);

    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

	var users = context._users.ToList();
	foreach (var user in users)
	{
		Console.WriteLine($"Login: {user.Login}, Password: {user.Password}, Role: {user.Role}");
	}
}

app.Run();