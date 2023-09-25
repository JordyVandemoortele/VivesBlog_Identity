using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;
using VivesBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VivesBlogDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
})
    //Roles are activated now
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VivesBlogDbContext>();

//Authorize
builder.Services.ConfigureApplicationCookie(options =>
{
    //Security
    options.Cookie.HttpOnly = true;
    //Path to go when not authorized
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccesDenied";
    //Logout automaticly after 5 minutes of inactivity
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
    database.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
