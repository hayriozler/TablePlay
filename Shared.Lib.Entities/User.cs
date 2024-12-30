namespace Shared.Lib.Entities;
public sealed class User(int Id, string firstName, string lastName, string userName, string password)
{
    public UserId Id { get; private set; } = new UserId(Id);
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string UserName { get; private set; } = userName;
    public string HashedPassword { get; private set; } = password;
}