using Shared.Lib.Entities;

namespace Shared.Lib.Accounts;
public sealed class Login(string Email, string Password) : BaseEntity
{
    public LoginId Id { get; private set; } = new LoginId(Guid.NewGuid());
    public string Email { get; private set; } = Email;
    public string Password { get; private set; } = Password;
}
