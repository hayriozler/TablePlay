namespace Shared.Lib.Services.Hasher;
internal interface IPasswordHasher
{
    string Hash(string Password);
    bool Verify(string Password, string PasswordHash);
}