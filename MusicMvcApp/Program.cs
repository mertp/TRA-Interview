using Microsoft.EntityFrameworkCore;
using MusicMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//Db contextini startup dosyasýna eklenmesi
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //Error sayfasýnýn custom error sayfasýna yönlendirilmesi
    app.UseExceptionHandler("/Music/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //error sayfasýnýn redirect iþlemi
    app.UseStatusCodePagesWithRedirects("/Error/{0}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Music}/{action=Index}/{id?}");

app.Run();
