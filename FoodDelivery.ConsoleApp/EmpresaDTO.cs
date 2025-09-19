using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.ConsoleApp
{
    public class EmpresaDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
    }
}