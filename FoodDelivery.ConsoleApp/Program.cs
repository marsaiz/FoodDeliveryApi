using System;
using System.Net.Http;
using System.Net.Http.Json;
using FoodDelivery.ConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== Food Delivery Console ===");
            Console.WriteLine("1. Crear Empresa");
            Console.WriteLine("2. Listar Empresas");
            Console.WriteLine("3. Crear Categoria");
            Console.WriteLine("4. Listar Categorias por empresa");
            Console.WriteLine("5. Crear Producto");
            Console.WriteLine("6. Listar Productos por categoria");
            Console.WriteLine("7. Crear Cliente");
            Console.WriteLine("8. Listar Clientes");
            Console.WriteLine("9. Crear Direccion para Cliente");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Crear Empresa seleccionado.");
                    await CrearEmpresaAsync();
                    break;
                case "2":
                    Console.WriteLine("Listar Empresas seleccionado.");
                    await ListarEmpresasAsync();
                    break;
                case "3":
                    Console.WriteLine("Crear Categoria seleccionado.");
                    break;
                case "4":
                    Console.WriteLine("Listar Categorias por empresa seleccionado.");
                    break;
                case "5":
                    Console.WriteLine("Crear Producto seleccionado.");
                    break;
                case "6":
                    Console.WriteLine("Listar Productos por categoria seleccionado.");
                    break;
                case "7":
                    Console.WriteLine("Crear Cliente seleccionado.");
                    break;
                case "8":
                    Console.WriteLine("Listar Clientes seleccionado.");
                    break;
                case "9":
                    Console.WriteLine("Crear Direccion para Cliente seleccionado.");
                    break;
                case "0":
                    salir = true;
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    break;
            }
            if (!salir)
            {
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            Console.WriteLine(); // Línea en blanco para mejor legibilidad
        }
    }


    // Métodos a implementar para cada funcionalidad:
    static async Task CrearEmpresaAsync()
    {
        Console.WriteLine("Ingrese los detalles de la empresa:");
        Console.Write("Nombre: ");
        var nombre = Console.ReadLine();
        Console.Write("Dirección: ");
        var direccion = Console.ReadLine();
        Console.Write("Teléfono: ");
        var telefono = Console.ReadLine();
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Latitud: ");
        var latitud = double.TryParse(Console.ReadLine(), out var lat) ? lat : (double?)null;
        Console.Write("Longitud: ");
        var longitud = double.TryParse(Console.ReadLine(), out var lon) ? lon : (double?)null;

        var empresa = new EmpresaDTO
        {
            Nombre = nombre,
            Direccion = direccion,
            Telefono = telefono,
            Email = email,
            Latitud = latitud,
            Longitud = longitud
        };

        using var httpClient = new HttpClient();
        var apiUrl = await httpClient.PostAsJsonAsync("http://localhost:5012/api/empresas", empresa);
        
        if (apiUrl.IsSuccessStatusCode)
        {
            Console.WriteLine("Empresa creada exitosamente.");
        }
        else
        {
            Console.WriteLine($"Error al crear la empresa: {apiUrl.ReasonPhrase}");
        }
    }

    static async Task ListarEmpresasAsync()
    {
        using var httpClient = new HttpClient();
        var empresas = await httpClient.GetFromJsonAsync<List<EmpresaDTO>>("http://localhost:5012/api/empresas");
        foreach (var emp in empresas)
        {
            Console.WriteLine($"Nombre: {emp.Nombre}");
        }
    }

    static async Task CrearCategoriaAsync()
    {
        // Lógica para crear una categoría
    }

    static async Task ListarCategoriasPorEmpresaAsync()
    {
        // Lógica para listar las categorías por empresa
    }

    static async Task CrearProductoAsync()
    {
        // Lógica para crear un producto
    }

    static async Task ListarProductosPorCategoriaAsync()
    {
        // Lógica para listar los productos por categoría
    }

    static async Task CrearClienteAsync()
    {
        // Lógica para crear un cliente
    }

    static async Task ListarClientesAsync()
    {
        // Lógica para listar los clientes
    }

    static async Task CrearDireccionParaClienteAsync()
    {
        // Lógica para crear una dirección para un cliente
    }
}