using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Servicios.Utils;

public static class PasswordHelper
{
public static string GenerateSalt()
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
    public static string HashPassword(string password, string salt) // Hashing simple, reemplazar con lógica real
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