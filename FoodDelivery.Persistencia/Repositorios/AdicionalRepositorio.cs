

namespace FoodDelivery.Persistencia.Repositorios;

public class AdicionalRepositorio : IAdicionalRepositorio
{
    private readonly ApplicationDbContext _context;

    public AdicionalRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<Adicional> CrearAdicionalAsync(Adicional adicional)
    {
        throw new NotImplementedException();
    }

    public Task<Adicional> ActualizarAdicionalAsync(Adicional adicional)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa)
    {
        throw new NotImplementedException();
    }

    public Task<Adicional> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa)
    {
        throw new NotImplementedException();
    }
}
