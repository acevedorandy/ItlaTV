
using ItlaTV.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using ItlaTV.IOC.Dependencies.dbo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ItlaTVContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ItlaTV")));

// Registro de las depedencias

builder.Services.AddDboDependency();
builder.Services.AddScoped<SerieMethods>();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
