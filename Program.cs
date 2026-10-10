// Importa las herramientas de Entity Framework Core.
using Microsoft.EntityFrameworkCore;

// Importa el contexto de base de datos del proyecto.
using WebApiProductos.Data;

// Crea el configurador de la aplicación.
var builder = WebApplication.CreateBuilder(args);

// Registra los controladores de la API.
builder.Services.AddControllers();

// Registra el contexto y configura la base de datos SQLite.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Guarda la base de datos en el archivo productos.db.
    options.UseSqlite("Data Source=productos.db");
});

// Registra la información de los endpoints para Swagger.
builder.Services.AddEndpointsApiExplorer();

// Registra el generador de documentación Swagger.
builder.Services.AddSwaggerGen();

// Construye la aplicación con los servicios registrados.
var app = builder.Build();

// Crea un ámbito temporal para utilizar el contexto de datos.
using (var scope = app.Services.CreateScope())
{
    // Obtiene el contexto de la base de datos.
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Crea la base de datos y sus tablas si todavía no existen.
    db.Database.EnsureCreated();
}

// Habilita Swagger cuando la aplicación está en desarrollo.
if (app.Environment.IsDevelopment())
{
    // Expone el documento que describe los endpoints.
    app.UseSwagger();

    // Habilita la interfaz para probar la API.
    app.UseSwaggerUI();
}

// Redirige las solicitudes HTTP hacia HTTPS.
app.UseHttpsRedirection();

// Usa index.html de wwwroot como página de inicio.
app.UseDefaultFiles();

// Permite servir los archivos HTML, CSS y JavaScript de wwwroot.
app.UseStaticFiles();

// Habilita las rutas de los controladores.
app.MapControllers();

// Inicia la aplicación y espera las solicitudes.
app.Run();