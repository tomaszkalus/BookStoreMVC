using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Services;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencySymbol = "€";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/Identity/Account/Login";
    o.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {

        List<Error> error = actionContext.ModelState
                    .Where(modelError => modelError.Value.Errors.Count > 0)
                    .Select(modelError => new Error
                    {
                        ErrorField = modelError.Key,
                        ErrorDescription = modelError.Value.Errors.FirstOrDefault().ErrorMessage
                    }).ToList();

        return new BadRequestObjectResult(error);
    };
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddSingleton<IImageService, ImageService>();

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
app.UseAuthentication();
//app.UseMiddleware<CustomAuthenticationMiddleware>();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.UseStatusCodePagesWithRedirects("/Error/{0}"); //point to error page
app.Run();