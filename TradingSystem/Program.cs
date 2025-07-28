using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using System.Globalization;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Infrastructure.Repositories;

using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Repositories;
using TrandingSystem.Application.Features.Video.Handlers;
using AutoMapper;
using NuGet.Protocol.Core.Types;
using MediatR;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");;

builder.Services.AddDbContext<db23617Context>(options =>
    options.UseSqlServer(connectionString)
           .UseLazyLoadingProxies()
);


//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(AddCourseCommand).Assembly));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<db23617Context>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetVideosByCourseIdHandler).Assembly);
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// four steps for add localization support
// dd DI for UnitOfWork and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 1-- add service Localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources"; // مكان ملفات .resx
});
// end step 1

//2--  to make me use localization 
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
// end step 2
builder.Services.AddHttpContextAccessor();

// 3-- define Language Can Use It In App
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // الثقافات المدعومة (مثلاً إنجليزي وعربي)
    var supportedCultures = new[] { 
        new CultureInfo("en"), 
        new CultureInfo("ar") ,    };

    options.DefaultRequestCulture = new RequestCulture("en"); // اللغة الافتراضية

    // اللغات اللي بيتم استخدامها في الترجمة
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    //// هنا بنحدد الطريقة اللي المستخدم يقدر يحدد بيها اللغة (كوكي أو كويري أو هيدر)
    //options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
// end step 3
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
// 4-- تفعيل دعم التعدد اللغوي
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
// end step 4
app.UseAuthentication(); // Added for Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Added for Identity

app.Run();
