using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Aplicacion.DTOs;
    public class AdicionalDTO
    {
        public int IdAdicional { get; set; }
        public string NombreAdicional { get; set; }
        public decimal PrecioAdicional { get; set; }
    }