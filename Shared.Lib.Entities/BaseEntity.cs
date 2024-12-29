namespace Shared.Lib.Entities;
public abstract class BaseEntity<T>
    where T : record
{

}
public record UserId(Guid Value);