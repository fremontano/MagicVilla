using MagicVilla_API;
using MagicVilla_API.Datos;
using MagicVilla_API.Repositorio;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(); //agregando servicio para el uso de httpacth
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//agregar mi nuevo servicios para la cadena de conecion de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
});

//servicio map para recorrer mis objecto
builder.Services.AddAutoMapper(typeof(MappingConfig));
//inyectar villa repositorio dentro de mi servicio
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();

//agregar villa repositorio y crear el servicio paar poderlo inyectar en el controlador
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
