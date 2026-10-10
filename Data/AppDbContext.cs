using Microsoft.EntityFrameworkCore; // Importa las herramientas de Entity Framework Core.
using WebApiProductos.Models; // Importa el modelo Producto.

namespace WebApiProductos.Data; // Define el espacio de nombres del contexto.

public class AppDbContext : DbContext // Define el contexto que administra la base de datos.
{ // Inicia la clase.
    public AppDbContext(DbContextOptions<AppDbContext> options) // Recibe la configuración de conexión.
        : base(options) // Entrega esa configuración a la clase base DbContext.
    { // Inicia el constructor.
    } // Finaliza el constructor.

    public DbSet<Producto> Productos => Set<Producto>(); // Permite consultar y modificar la tabla Productos.
} // Finaliza la clase.