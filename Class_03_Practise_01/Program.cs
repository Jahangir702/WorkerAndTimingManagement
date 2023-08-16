using Class_03_Practise_01.HostedServices;
using Class_03_Practise_01.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WorkerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddHostedService<DbSeederHostedService>();
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();
