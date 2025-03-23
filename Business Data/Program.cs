using Business_Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra el contexto para la base de datos
builder.Services.AddDbContext<DatosEmpresasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatosEmpresasConnection")));

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
