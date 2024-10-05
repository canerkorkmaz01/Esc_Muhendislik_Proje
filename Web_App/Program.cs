using Microsoft.AspNetCore.Identity;
using static Web_App_Data.Interface.IRepository;
using Web_App_Data.DbContext;
using Web_App_Data.Repository;
using Web_App_Data.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5206/");
});

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;           // Rakam zorunlulu�u kapat�l�yor
    options.Password.RequireLowercase = false;       // K���k harf zorunlulu�u kapat�l�yor
    options.Password.RequireUppercase = false;       // B�y�k harf zorunlulu�u kapat�l�yor
    options.Password.RequireNonAlphanumeric = false; // �zel karakter zorunlulu�u kapat�l�yor
    options.Password.RequiredLength = 4;             // Minimum �ifre uzunlu�u 4 olarak ayarland�
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ProductService>();
builder.Services.AddHttpClient();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();
    await context.SeedData(userManager, roleManager);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/Account/Login");
    return Task.CompletedTask;
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
