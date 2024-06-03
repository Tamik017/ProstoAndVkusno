using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProstoAndVkusno.Interfaces;
using ProstoAndVkusno.mocks;

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
builder.Services.AddTransient<IAllProducts, MockProducts>();
builder.Services.AddTransient<IProductCategory, MockCategory>();

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
	var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

	var users = context._users.ToList();
	foreach (var user in users)
	{
		Console.WriteLine($"Login: {user.Login}, Password: {user.Password}, Role: {user.Role}");
	}
}

app.Run();
//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc();

//var app = builder.Build();

//app.UseStaticFiles();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}"
//    );
//app.Run();
