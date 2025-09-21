using System;
using System.Net.Http;
using System.Net.Http.Json;
using FoodDelivery.ConsoleApp;
using FoodDelivery.ConsoleApp.Servicios;

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
                    await new EmpresaServicioConsola(new HttpClient()).CrearEmpresaAsync();
                    break;
                case "2":
                    Console.WriteLine("Listar Empresas seleccionado.");
                    await new EmpresaServicioConsola(new HttpClient()).ListarEmpresasAsync();
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
}