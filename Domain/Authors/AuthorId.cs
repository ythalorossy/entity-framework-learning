using Domain.Common;

namespace Domain.Authors;

public class AuthorId : ValueObject
{
    public AuthorId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AuthorId New()
    {
        return new AuthorId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}