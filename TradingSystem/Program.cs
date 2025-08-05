using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Infrastructure.Repositories;
using TrandingSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ربط قاعدة البيانات
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<db23617Context>(options =>
    options.UseSqlServer(connectionString)
           .UseLazyLoadingProxies());

// تسجيل AutoMapper و MediatR والخدمات الأخرى
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TrandingSystem.Application.Features.Courses.Commands.AddCourseCommand).Assembly));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFileService, FileService>();
//builder.Services.AddScoped<IWasabiUploader, WasabiUploader>();
// Identity مع Roles و EF Store
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<db23617Context>();

// Localization - ملف الموارد داخل Resources/
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Controllers + Localization + FluentValidation (مسجل Validator يدوياً)
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//builder.Services.AddTransient<IValidator<VideoAddedDto>, VideoAddDtoVaildator>();
//builder.Services.AddTransient<IValidator<ViedoUpdateDto>, VideoUpdateDtoValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<VideoUpdateDtoValidator>();


builder.Services.AddHttpContextAccessor();

// إعداد اللغات المدعومة
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("ar"), new CultureInfo("en") };
    options.DefaultRequestCulture = new RequestCulture("ar");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// For Expend Size For Request
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500_000_000; // مثلاً 500 ميجا
});

// Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 500_000_000; // 500 ميجا
});
var app = builder.Build();

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
