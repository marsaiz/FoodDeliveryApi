using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;
using static FoodDelivery.Servicios.Utils.PasswordHelper;

namespace FoodDelivery.Servicios.Servicios;

public class EmpresaServicio : IEmpresaServicio
{
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public EmpresaServicio(IEmpresaRepositorio empresaRepositorio)
    {
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<EmpresaDTO> CrearEmpresaAsync(EmpresaCreateDTO empresaDTO)
    {

    // Verifica si el usuario o email ya existen
        var empresaExistentePorUsuario = await _empresaRepositorio.ObtenerEmpresaPorUsuarioAsync(empresaDTO.Usuario);
        if (empresaExistentePorUsuario != null)
            throw new Exception("El usuario ya está en uso.");

        var empresaExistentePorEmail = await _empresaRepositorio.ObtenerEmpresaPorEmailAsync(empresaDTO.Email);
        if (empresaExistentePorEmail != null)
            throw new Exception("El email ya está en uso.");

        // Genera el hash y el salt de la contraseña
        string salt = GenerateSalt();
        string passwordHash = HashPassword(empresaDTO.Password, salt);

        // Crea la entidad Empresa
        var empresa = new Empresa
        {
            IdEmpresa = Guid.NewGuid(),
            Nombre = empresaDTO.Nombre,
            Direccion = empresaDTO.Direccion,
            Telefono = empresaDTO.Telefono,
            Email = empresaDTO.Email,
            Latitud = empresaDTO.Latitud,
            Longitud = empresaDTO.Longitud,
            Usuario = empresaDTO.Usuario,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
        };

        await _empresaRepositorio.CrearEmpresaAsync(empresa);
        return new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Direccion = empresa.Direccion,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            Usuario = empresa.Usuario
        };
    }

    public async Task<EmpresaDTO> ObtenerEmpresaPorUsuarioAsync(string usuario)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorUsuarioAsync(usuario);
        if (empresa == null) return null;
        return new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Direccion = empresa.Direccion,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            Usuario = empresa.Usuario
        };
    }

    public async Task<EmpresaDTO> ObtenerEmpresaPorEmailAsync(string email)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorEmailAsync(email);
        if (empresa == null) return null;
        return new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Direccion = empresa.Direccion,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            Usuario = empresa.Usuario
        };
    }

    public async Task<EmpresaDTO> ActualizarEmpresaAsync(EmpresaUpdateDTO empresaDTO)
    {
        var empresaExistente = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(empresaDTO.IdEmpresa);
        if (empresaExistente == null)
            throw new Exception("Empresa no encontrada.");

        empresaExistente.Nombre = empresaDTO.Nombre;
        empresaExistente.Direccion = empresaDTO.Direccion;
        empresaExistente.Telefono = empresaDTO.Telefono;
        empresaExistente.Email = empresaDTO.Email;

        await _empresaRepositorio.ActualizarEmpresaAsync(empresaExistente);
        return new EmpresaDTO
        {
            IdEmpresa = empresaExistente.IdEmpresa,
            Nombre = empresaExistente.Nombre,
            Direccion = empresaExistente.Direccion,
            Telefono = empresaExistente.Telefono,
            Email = empresaExistente.Email,
            Latitud = empresaExistente.Latitud,
            Longitud = empresaExistente.Longitud,
            Usuario = empresaExistente.Usuario
        };
    }

    public async Task<bool> EliminarEmpresaAsync(Guid idEmpresa)
    {
        return await _empresaRepositorio.EliminarEmpresaAsync(idEmpresa);
    }

    public async Task<EmpresaDTO> ObtenerEmpresaPorIdAsync(Guid idEmpresa)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null) return null;
        return new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Direccion = empresa.Direccion,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            Usuario = empresa.Usuario
        };
    }

    public async Task<List<EmpresaDTO>> ObtenerEmpresasAsync()
    {
        var empresas = await _empresaRepositorio.ObtenerEmpresasAsync();
        return empresas.Select(e => new EmpresaDTO
        {
            IdEmpresa = e.IdEmpresa,
            Nombre = e.Nombre,
            Direccion = e.Direccion,
            Telefono = e.Telefono,
            Email = e.Email,
            Latitud = e.Latitud,
            Longitud = e.Longitud,
            Usuario = e.Usuario
        }).ToList();
    }

    public async Task<bool> CambiarPasswordAsync(Guid idEmpresa, EmpresaChangePasswordDTO dto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null)
            throw new Exception("Empresa no encontrada.");

        // Verifica la contraseña actual
        if ((empresa.PasswordHash ?? string.Empty) != HashPassword(dto.PasswordActual, empresa.PasswordSalt ?? string.Empty)) // Asumiendo que tienes un método de hashing
            throw new Exception("La contraseña actual es incorrecta.");


        // Actualiza la contraseña (deberías hashearla antes de guardarla)
        string nuevoPasswordSalt = GenerateSalt();
        string nuevoPasswordHash = HashPassword(dto.NuevoPassword, nuevoPasswordSalt);

        // Ajusta la llamada según la firma correcta del método CambiarPasswordAsync
        // Por ejemplo, si el método acepta solo el id y el nuevo hash:
        return await _empresaRepositorio.CambiarPasswordAsync(idEmpresa, nuevoPasswordHash);
        // Si requiere un objeto DTO, usa:
        // return await _empresaRepositorio.CambiarPasswordAsync(new CambiarPasswordDTO { IdEmpresa = dto.IdEmpresa, PasswordHash = nuevoPasswordHash, PasswordSalt = nuevoPasswordSalt });
    }   

    public async Task<EmpresaDTO?> LoginAsync(EmpresaLoginDTO loginDTO)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorUsuarioAsync(loginDTO.Usuario);
        if (empresa == null)
            return null;
        // Verifica el hash de la contraseña (ajusta según tu lógica de hash)
        if (!VerifyPassword(loginDTO.Password, empresa.PasswordHash ?? string.Empty, empresa.PasswordSalt ?? string.Empty))
            return null;
        return new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Direccion = empresa.Direccion,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            EstaAbierta = empresa.EstaAbierta,
            Usuario = empresa.Usuario
        };
    }
}
