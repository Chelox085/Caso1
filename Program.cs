using Caso1.Data;
using Caso1.Models;
using Caso1.ModelBinders;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CustomDateModelBinderProvider());
});

builder.Services.AddDbContext<Caso1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CASO_PRACTICO_RESERVACIONES")));

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./LlavesDeSeguridad"));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit     = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength   = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<Caso1Context>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedData.InicializarAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();