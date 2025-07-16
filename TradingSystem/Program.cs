using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
// four steps for add localization support

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
