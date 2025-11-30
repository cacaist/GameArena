using GameArena.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQLite baðlantýsý
builder.Services.AddDbContext<GameArenaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GameArenaContext")));

// MVC desteði
builder.Services.AddControllersWithViews();

// Session middleware'i
builder.Services.AddSession();

var app = builder.Build();

// Geliþtirme dýþý ortamlarda hata sayfasý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();      
app.UseRouting();
app.UseSession();     
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
