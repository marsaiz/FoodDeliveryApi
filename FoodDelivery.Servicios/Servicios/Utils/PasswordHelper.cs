using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Servicios.Utils;

public static class PasswordHelper
{
    // Verifica si el password ingresado coincide con el hash y salt almacenados
    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var hashOfInput = HashPassword(password, storedSalt);
        return hashOfInput == storedHash;
    }

    public static string GenerateSalt()
    {
        // Genera un salt aleatorio (implementación simple, usa una más robusta en producción)
        // Salt es un valor aleatorio que se añade a la contraseña antes de hashearla
        // para aumentar la seguridad contra ataques de diccionario y rainbow tables.
        byte[] saltBytes = new byte[16];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    // Método simple de hashing de contraseña (usa una implementación real en producción)
    // Hashing es un proceso de convertir la contraseña en una cadena fija de caracteres
    // que no puede ser revertida a la contraseña original.
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