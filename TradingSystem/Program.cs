using AutoMapper;
using MediatR;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using System.Globalization;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Validators;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Infrastructure.Repositories;

using TrandingSystem.Domain.Interfaces;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");;
using TrandingSystem.Infrastructure.Services;

// ربط قاعدة البيانات
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<db23617Context>(options =>
    options.UseSqlServer(connectionString)
           .UseLazyLoadingProxies());

// تسجيل AutoMapper و MediatR والخدمات الأخرى
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(AddCourseCommand).Assembly));

//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


//builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TrandingSystem.Application.Features.Courses.Commands.AddCourseCommand).Assembly));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IImageService, ImageService>();

// Identity مع Roles و EF Store
    .AddEntityFrameworkStores<db23617Context>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Localization - ملف الموارد داخل Resources/
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//builder.Services.AddTransient<IValidator<ViedoUpdateDto>, VideoUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VideoUpdateDtoValidator>();

builder.Services.AddHttpContextAccessor();

// إعداد اللغات المدعومة
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("ar"), new CultureInfo("en") };
    options.DefaultRequestCulture = new RequestCulture("ar");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

    //// هنا بنحدد الطريقة اللي المستخدم يقدر يحدد بيها اللغة (كوكي أو كويري أو هيدر)
    //options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
// Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// تفعيل دعم التعدد اللغوي
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.MapRazorPages();

app.Run();
