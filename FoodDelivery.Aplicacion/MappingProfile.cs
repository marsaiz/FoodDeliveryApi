using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Aplicacion;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Aquí defines tus mapeos
        CreateMap<Adicional, DTOs.AdicionalDTO>().ReverseMap();

    }
}