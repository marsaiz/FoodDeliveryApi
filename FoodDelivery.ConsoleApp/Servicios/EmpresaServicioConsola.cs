using System.Net.Http;
using System.Net.Http.Json;
using FoodDelivery.Servicios.DTOs;


namespace FoodDelivery.ConsoleApp.Servicios
{
    public class EmpresaServicioConsola
    {
        private readonly HttpClient _httpClient;

        public EmpresaServicioConsola(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CrearEmpresaAsync()
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

            var empresa = new EmpresaCreateDTO
            {
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
                Email = email,
                Latitud = latitud.HasValue ? (decimal?)latitud.Value : null,
                Longitud = longitud.HasValue ? (decimal?)longitud.Value : null
            };

            var apiUrl = await _httpClient.PostAsJsonAsync("http://localhost:5012/api/empresas", empresa);

            if (apiUrl.IsSuccessStatusCode)
            {
                Console.WriteLine("Empresa creada exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error al crear la empresa: {apiUrl.ReasonPhrase}");
            }
        }
        public async Task ListarEmpresasAsync()
        {
            using var httpClient = new HttpClient();
            var empresas = await httpClient.GetFromJsonAsync<List<EmpresaDTO>>("http://localhost:5012/api/empresas");
            foreach (var emp in empresas)
            {
                Console.WriteLine($"Nombre: {emp.Nombre}");
            }
        }
    }
}