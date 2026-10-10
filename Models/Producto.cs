using System.ComponentModel.DataAnnotations; // Importa los atributos para validar datos.

namespace WebApiProductos.Models; // Define el espacio de nombres del modelo.

public class Producto // Declara la clase que representa un producto.
{ // Inicia el cuerpo de la clase.
    public int Id { get; set; } // Identificador único; EF Core lo reconoce como clave primaria.

    [Required] // Indica que el nombre es obligatorio.
    [StringLength(100)] // Limita el nombre a un máximo de 100 caracteres.
    public string Nombre { get; set; } = string.Empty; // Permite leer y modificar el nombre; inicia vacío.

    [Range(typeof(decimal), "0.01", "999999999")] // Establece el precio permitido entre 0.01 y 999999999.
    public decimal Precio { get; set; } // Guarda el precio como decimal y permite leerlo y modificarlo.

    [Range(0, int.MaxValue)] // Permite un stock desde cero hasta el máximo valor de un entero.
    public int Stock { get; set; } // Guarda la cantidad disponible y permite leerla y modificarla.
} // Finaliza el cuerpo de la clase.