using System.Security.Cryptography;

namespace Shared.Lib.Services.Hasher;

internal class PasswordHasher : IPasswordHasher
{
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;
    private const int ITERATIONS = 1000;
    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _algorithm, HASH_SIZE);
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }
    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _algorithm, HASH_SIZE);
        return CryptographicOperations.FixedTimeEquals(hash, testHash);
    }
}