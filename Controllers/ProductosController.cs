using Microsoft.AspNetCore.Http; // Importa los códigos de respuesta HTTP.
using Microsoft.AspNetCore.Mvc; // Importa las herramientas para crear controladores.
using Microsoft.EntityFrameworkCore; // Importa las herramientas de Entity Framework Core.
using WebApiProductos.Data; // Importa el contexto de la base de datos.
using WebApiProductos.Models; // Importa el modelo Producto.

namespace WebApiProductos.Controllers; // Define el espacio de nombres.

[ApiController] // Activa la validación automática del modelo recibido.
[Route("api/[controller]")] // Define la ruta /api/Productos.
public class ProductosController : ControllerBase // Declara el controlador.
{ // Inicia la clase.
    private readonly AppDbContext _context; // Almacena el contexto de la base de datos.

    public ProductosController(AppDbContext context) // Recibe el contexto mediante inyección de dependencias.
    { // Inicia el constructor.
        _context = context; // Guarda el contexto recibido.
    } // Termina el constructor.

    [HttpGet] // Permite consultar todos los productos o buscar por nombre.
    [ProducesResponseType(typeof(List<Producto>), StatusCodes.Status200OK)] // Documenta la respuesta 200.
    public async Task<ActionResult<List<Producto>>> ObtenerTodos(
        [FromQuery] string? nombre = null) // Recibe un nombre opcional desde la URL.
    { // Inicia el método.
        var consulta = _context.Productos.AsNoTracking(); // Prepara una consulta sin seguimiento de cambios.

        if (!string.IsNullOrWhiteSpace(nombre)) // Comprueba si hay un texto de búsqueda.
        { // Inicia el filtro.
            var texto = nombre.Trim().ToLower(); // Quita espacios en los extremos y convierte a minúsculas.
            consulta = consulta.Where(p => p.Nombre.ToLower().Contains(texto)); // Compara ambos textos en minúsculas.
        } // Termina el filtro.

        var productos = await consulta.OrderBy(p => p.Id).ToListAsync(); // Consulta la base de datos y ordena por ID.
        return Ok(productos); // Devuelve la lista con código HTTP 200.
    } // Termina el método.

    [HttpGet("{id:int}")] // Permite consultar un producto por su ID.
    [ProducesResponseType(typeof(Producto), StatusCodes.Status200OK)] // Documenta la respuesta 200.
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Documenta la respuesta 404.
    public async Task<ActionResult<Producto>> ObtenerPorId(int id) // Recibe el ID solicitado.
    { // Inicia el método.
        var producto = await _context.Productos.FindAsync(id); // Busca el producto en la base de datos.

        if (producto == null) // Comprueba si el producto no existe.
        { // Inicia la condición.
            return NotFound(); // Devuelve código HTTP 404.
        } // Termina la condición.

        return Ok(producto); // Devuelve el producto con código HTTP 200.
    } // Termina el método.

    [HttpPost] // Permite crear un producto.
    [ProducesResponseType(typeof(Producto), StatusCodes.Status201Created)] // Documenta la respuesta 201.
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Documenta los errores de validación.
    public async Task<ActionResult<Producto>> Crear(Producto entrada) // Recibe los datos del nuevo producto.
    { // Inicia el método.
        if (string.IsNullOrWhiteSpace(entrada.Nombre)) // Comprueba que el nombre no esté vacío.
        { // Inicia la validación.
            ModelState.AddModelError(nameof(Producto.Nombre), "El nombre es obligatorio."); // Registra el error del nombre.
            return ValidationProblem(ModelState); // Devuelve los errores con código HTTP 400.
        } // Termina la validación.

        var producto = new Producto // Crea un producto sin utilizar el ID enviado por el cliente.
        { // Inicia las propiedades del producto.
            Nombre = entrada.Nombre.Trim(), // Guarda el nombre sin espacios en los extremos.
            Precio = entrada.Precio, // Guarda el precio recibido.
            Stock = entrada.Stock // Guarda la cantidad disponible.
        }; // Termina la creación del producto.

        _context.Productos.Add(producto); // Prepara la inserción del producto.
        await _context.SaveChangesAsync(); // Guarda el producto en la base de datos.
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto); // Devuelve HTTP 201 y la URL del producto creado.
    } // Termina el método.

    [HttpPut("{id:int}")] // Permite actualizar un producto por su ID.
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Documenta la respuesta 204.
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Documenta los errores de validación.
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Documenta la respuesta 404.
    public async Task<IActionResult> Actualizar(int id, Producto entrada) // Recibe el ID y los nuevos datos.
    { // Inicia el método.
        if (string.IsNullOrWhiteSpace(entrada.Nombre)) // Comprueba que el nombre no esté vacío.
        { // Inicia la validación.
            ModelState.AddModelError(nameof(Producto.Nombre), "El nombre es obligatorio."); // Registra el error del nombre.
            return ValidationProblem(ModelState); // Devuelve los errores con código HTTP 400.
        } // Termina la validación.

        var producto = await _context.Productos.FindAsync(id); // Busca el producto que se actualizará.

        if (producto == null) // Comprueba si el producto no existe.
        { // Inicia la condición.
            return NotFound(); // Devuelve código HTTP 404.
        } // Termina la condición.

        producto.Nombre = entrada.Nombre.Trim(); // Actualiza el nombre sin espacios en los extremos.
        producto.Precio = entrada.Precio; // Actualiza el precio.
        producto.Stock = entrada.Stock; // Actualiza la cantidad disponible.

        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return NoContent(); // Devuelve código HTTP 204 sin cuerpo.
    } // Termina el método.

    [HttpDelete("{id:int}")] // Permite eliminar un producto por su ID.
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Documenta la respuesta 204.
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Documenta la respuesta 404.
    public async Task<IActionResult> Eliminar(int id) // Recibe el ID del producto.
    { // Inicia el método.
        var producto = await _context.Productos.FindAsync(id); // Busca el producto que se eliminará.

        if (producto == null) // Comprueba si el producto no existe.
        { // Inicia la condición.
            return NotFound(); // Devuelve código HTTP 404.
        } // Termina la condición.

        _context.Productos.Remove(producto); // Prepara la eliminación del producto.
        await _context.SaveChangesAsync(); // Elimina el producto de la base de datos.
        return NoContent(); // Devuelve código HTTP 204 sin cuerpo.
    } // Termina el método.
} // Termina la clase.