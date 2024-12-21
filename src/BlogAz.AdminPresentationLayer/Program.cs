using BlogAz.Facade;
using BlogAz.Infrastructure;
using BlogAz.Application;
using BlogAz.AdminPresentationLayer.Infrastructure.Middleware;
using BlogAz.AdminPresentationLayer.Infrastructure.JwtUtil;
using BlogAz.AdminPresentationLayer.Infrastructure;

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

app.UseMiddleware<RedirectToLoginMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
