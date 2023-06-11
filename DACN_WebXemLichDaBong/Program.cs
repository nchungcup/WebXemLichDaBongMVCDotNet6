using DACN_WebXemLichDaBong.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("Xemlichdabong"); builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        // Cấu hình các tuỳ chọn khác của JSON.NET (nếu cần)
    });
builder.Services.AddMvc();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(24); });
builder.Services.AddMvc().AddControllersAsServices();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);
var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseMvc();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseMvc(routes =>
{
    routes.MapRoute(
      name: "areas",
      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
