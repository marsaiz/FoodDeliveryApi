using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Servicios.DTOs;

public class ClienteCreateDTO
{
    public string NombreCliente { get; set; }
    public string? TelefonoCliente { get; set; }
    public string? EmailCliente { get; set; }
}
// Note: Changed Nombre to NombreCliente to match the domain model.