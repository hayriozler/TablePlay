using Shared.Lib.Entities;

namespace Shared.Lib.Accounts;
public sealed class Login(string id, string email, string password) : BaseEntity(id)
{
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
}
