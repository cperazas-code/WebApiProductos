# CHOCOLATO — API de productos

Web API desarrollada con ASP.NET Core y Entity Framework Core para administrar productos. Incluye una interfaz web que permite consultar, crear, modificar y eliminar productos.

## Tecnologías

- .NET 10 y C#.
- ASP.NET Core con controladores.
- Entity Framework Core y SQLite.
- Swagger para probar los endpoints.
- HTML, CSS y JavaScript para la interfaz web.

## Funcionalidades

- Consultar todos los productos.
- Buscar productos por nombre.
- Consultar un producto por su identificador.
- Registrar productos.
- Modificar nombre, precio y stock.
- Eliminar productos.
- Validar los datos recibidos.

## Ejecutar el proyecto

Requisito: tener instalado el SDK de .NET 10 y Git.

Abre una terminal y ejecuta:

```bash
git clone https://github.com/cperazas-code/WebApiProductos.git
cd WebApiProductos
dotnet restore
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

Mantén la terminal abierta mientras utilizas la aplicación.

- Interfaz web: https://localhost:7070
- Swagger: https://localhost:7070/swagger

La base de datos SQLite se crea automáticamente al iniciar la aplicación. El archivo local `productos.db` está excluido del repositorio.

## Endpoints

| Método | Ruta | Operación |
|--------|------|-----------|
| GET | /api/Productos | Consultar productos |
| GET | /api/Productos?nombre=chocolate | Buscar por nombre |
| GET | /api/Productos/{id} | Consultar por identificador |
| POST | /api/Productos | Crear un producto |
| PUT | /api/Productos/{id} | Actualizar un producto |
| DELETE | /api/Productos/{id} | Eliminar un producto |

## Ejemplo para crear un producto

```json
{
  "nombre": "Chocolate artesanal",
  "precio": 25,
  "stock": 10
}
```

## Estructura del proyecto

- `Controllers`: endpoints de la API.
- `Models`: modelo de producto y validaciones.
- `Data`: contexto de Entity Framework Core.
- `wwwroot`: interfaz web.
- `Program.cs`: configuración de servicios y aplicación.

## Video explicativo

Pendiente de agregar el enlace al video de demostración, con duración máxima de 3 minutos.