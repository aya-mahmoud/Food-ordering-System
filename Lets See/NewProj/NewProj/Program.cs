//using Autofac.Core;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Build.Evaluation.Project.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using NewProj.Data;
using Microsoft.Extensions.DependencyInjection;
//using NewProj.Services.AspNetCoreEmailConfirmationSendGrid.Services;
using NewProj.Models;
using NewProj.Classes;
using NewProj.Models.Interfaces;
using NewProj.Models.Mocks;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;



//admin

builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDbContextPool<ProjectContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Project;Integrated Security=True");
    optionsBuilder.UseInternalServiceProvider(serviceProvider);
});





// admin 2

builder.Services.AddScoped<IProductRepository, MockProductRepository>();
builder.Services.AddScoped<IOrderRepository, MockOrderRepository>();
builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<ICartRepository, MockCartRepository>();
builder.Services.AddScoped<IUserRepository, MockUserRepository>();



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


/// <summary>
/// new
/// </summary>
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders()

//    .AddDefaultUI();





builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders().AddDefaultUI();

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.SignIn.RequireConfirmedAccount = true;
    opts.SignIn.RequireConfirmedEmail = true;

});


//builder.Services.AddTransient<IEmailSender, EmailSender>();





/// <summary>
/// new
/// </summary>
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = $"/Identity/account/logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

});


builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication()




.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})


.AddFacebook(options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];

});

builder.Services.AddHealthChecks();
builder.Services.AddMvc();
//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

StripeConfiguration.ApiKey = "sk_test_51KfxKmBfomHHapStZZTEn8knMN6K1k5MAx8ct3qXt6yjooiGwVfjeGZDjReS44tFsdC1qBR0pUEuSqjpNwdS7Ynk00ypvgF2qZ";





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller}/{action}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.MapRazorPages();

app.Run();
