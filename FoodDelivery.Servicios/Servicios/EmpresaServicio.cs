using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class EmpresaServicio : IEmpresaServicio
{
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public EmpresaServicio(IEmpresaRepositorio empresaRepositorio)
    {
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<Empresa> CrearEmpresaAsync(EmpresaCreateDTO empresaDTO)
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
        return empresa;
    }

    public async Task<Empresa> ObtenerEmpresaPorUsuarioAsync(string usuario)
    {
        return await _empresaRepositorio.ObtenerEmpresaPorUsuarioAsync(usuario);
    }

    public async Task<Empresa> ObtenerEmpresaPorEmailAsync(string email)
    {
        return await _empresaRepositorio.ObtenerEmpresaPorEmailAsync(email);
    }

    public async Task<Empresa> ActualizarEmpresaAsync(EmpresaUpdateDTO empresaDTO)
    {
        var empresaExistente = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(empresaDTO.IdEmpresa);
        if (empresaExistente == null)
            throw new Exception("Empresa no encontrada.");

        empresaExistente.Nombre = empresaDTO.Nombre;
        empresaExistente.Direccion = empresaDTO.Direccion;
        empresaExistente.Telefono = empresaDTO.Telefono;
        empresaExistente.Email = empresaDTO.Email;

        await _empresaRepositorio.ActualizarEmpresaAsync(empresaExistente);
        return empresaExistente;
    }

    public async Task<bool> EliminarEmpresaAsync(Guid idEmpresa)
    {
        return await _empresaRepositorio.EliminarEmpresaAsync(idEmpresa);
    }

    public async Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa)
    {
        return await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
    }

    public async Task<List<Empresa>> ObtenerEmpresasAsync()
    {
        return await _empresaRepositorio.ObtenerEmpresasAsync();
    }

    public async Task<bool> CambiarPasswordAsync(EmpresaChangePasswordDTO dto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(dto.IdEmpresa);
        if (empresa == null)
            throw new Exception("Empresa no encontrada.");

        // Verifica la contraseña actual
        if (empresa.PasswordHash != HashPassword(dto.PasswordActual, empresa.PasswordSalt)) // Asumiendo que tienes un método de hashing
            throw new Exception("La contraseña actual es incorrecta.");


        // Actualiza la contraseña (deberías hashearla antes de guardarla)
        string nuevoPasswordSalt = GenerateSalt();
        string nuevoPasswordHash = HashPassword(dto.NuevoPassword, nuevoPasswordSalt);

        // Ajusta la llamada según la firma correcta del método CambiarPasswordAsync
        // Por ejemplo, si el método acepta solo el id y el nuevo hash:
        return await _empresaRepositorio.CambiarPasswordAsync(dto.IdEmpresa, nuevoPasswordHash);
        // Si requiere un objeto DTO, usa:
        // return await _empresaRepositorio.CambiarPasswordAsync(new CambiarPasswordDTO { IdEmpresa = dto.IdEmpresa, PasswordHash = nuevoPasswordHash, PasswordSalt = nuevoPasswordSalt });
    }

    private string GenerateSalt()
    {
        // Genera un salt aleatorio (implementación simple, usa una más robusta en producción)
        byte[] saltBytes = new byte[16];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }
    // Método simple de hashing de contraseña (usa una implementación real en producción)
    private string HashPassword(string password, string salt) // Hashing simple, reemplazar con lógica real
    {
        // Ejemplo simple usando SHA256, reemplaza por tu lógica real de hashing y salting
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var combined = password + salt;
            var bytes = System.Text.Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
