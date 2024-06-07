using Microsoft.EntityFrameworkCore;
using ProstoAndVkusno.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.mocks;
using ProstoAndVkusno.Data.Repository;
using ProstoAndVkusno.Data;
using ProstoAndVkusno.Data.Models;

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
builder.Services.AddTransient<IAllOrders, OrdersRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShopCart.GetCart(sp));
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// ������������ HTTP-��������
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
	// �������� ������� �� ���������
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	// ������� ��� ���������
	endpoints.MapControllerRoute(
		name: "categoryFilter",
		pattern: "Product/{action}/{category}",
		defaults: new { Controller = "Product", action = "List" });
});

//app.MapControllerRoute(
//	name: "default",
//	pattern: "{controller=Home}/{action=Index}/{id?}"
//	);

//app.UseMvc(routes =>
//{
//	routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
//	routes.MapRoute(name: "categoryFilter", template: "Product/{action}/{category}", defaults: new {Controller = "Product", action = "List"});
//});

// ������������� ���� ������
using (var scope = app.Services.CreateScope())
{
    ApplicationContext content = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
	DBObject.Initial(content);

    var ucontext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

	var users = ucontext._users.ToList();
	foreach (var user in users)
	{
		Console.WriteLine($"Login: {user.Login}, Password: {user.Password}, Role: {user.Role}");
	}
}

app.Run();