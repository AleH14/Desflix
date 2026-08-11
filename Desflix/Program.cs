using Microsoft.EntityFrameworkCore;
using Desflix.Data;
using Desflix.Data.UnitOfWork;
using Desflix.Core.Interfaces;
using Desflix.Business.Services;
using Desflix.Business.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar DbContext
builder.Services.AddDbContext<PeliculasDbContext>(item =>
    item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Patrón: Dependency Injection
// Registrar Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar Services
builder.Services.AddScoped<IPeliculaService, PeliculaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

// Registrar Factories
builder.Services.AddScoped<IPeliculaFactory, PeliculaFactory>();
builder.Services.AddScoped<IGeneroFactory, GeneroFactory>();

// Registrar Logging
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "Saludar",
    pattern: "{controller}/{action}/{nombre}/{id}");

app.Run();

