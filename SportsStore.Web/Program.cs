using Microsoft.EntityFrameworkCore;
using NLog.Web;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Web.Models;
using SportsStore.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Logging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Information);
}).UseNLog();



builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreDb"]);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
    name: "catpage",
    pattern: "{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" }
    );

app.MapControllerRoute(
    name: "page",
    pattern: "Page{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 }
    );

app.MapControllerRoute(
    name: "category",
    pattern: "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 }
    );

app.MapControllerRoute(
    name: "pagination",
    pattern: "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 }
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage(pattern: "/admin/{*catchall}", page: "/Admin/Index");

// Seed dummy data
//SeedData.Populate(app);

app.Run();
