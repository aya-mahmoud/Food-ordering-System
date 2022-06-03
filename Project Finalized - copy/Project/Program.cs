using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Stripe;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjectContextConnection");

builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProjectContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
options.IdleTimeout = TimeSpan.FromMinutes(30));

//builder.Services.AddAuthentication()
//    .AddGoogle("google", opt =>
//    {
//        var googleAuth = builder.Configuration.GetSection("Authentication:Google");
//        opt.ClientId = googleAuth["226694571705-ipgar13mulb9abj0i0vugf0hj1ed9564.apps.googleusercontent.com"];
//        opt.ClientSecret = googleAuth["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"];
//        opt.SignInScheme = IdentityConstants.ExternalScheme;
//    });

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = builder.Configuration["226694571705-ipgar13mulb9abj0i0vugf0hj1ed9564.apps.googleusercontent.com"];
//    googleOptions.ClientSecret = builder.Configuration["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"];
//});

//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    facebookOptions.AppId = builder.Configuration["718107432533616"];
//    facebookOptions.AppSecret = builder.Configuration["0681e0b9cf5a27815d078ef0aca5f1a4"];
//});
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


StripeConfiguration.ApiKey = "sk_test_51KfxKmBfomHHapStZZTEn8knMN6K1k5MAx8ct3qXt6yjooiGwVfjeGZDjReS44tFsdC1qBR0pUEuSqjpNwdS7Ynk00ypvgF2qZ";


/*
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["226694571705-ipgar13mulb9abj0i0vugf0hj1ed9564.apps.googleusercontent.com"];
       options.ClientSecret = googleAuthNSection["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"];
   })
   .AddFacebook(options =>
   {
       IConfigurationSection FBAuthNSection = config.GetSection("Authentication:FB");
       options.ClientId = FBAuthNSection["ClientId"];
       options.ClientSecret = FBAuthNSection["ClientSecret"];
   });
*/


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

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
