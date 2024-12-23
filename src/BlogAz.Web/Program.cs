using BlogAz.Facade;
using BlogAz.Application;
using BlogAz.Infrastructure;
using BlogAz.Web.Infrastructure;
using BlogAz.Web.Infrastructure.JwtUtil;
using BlogAz.Web.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.RegisterWebDependencies().InitFacadeDependencies().InitInfrastructureDependencies(builder.Configuration).InitApplicationDependencies();
builder.Services.AddMvc().AddRazorRuntimeCompilation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["Token"]?.ToString();
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});
app.UseMiddleware<RedirectToLoginMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
