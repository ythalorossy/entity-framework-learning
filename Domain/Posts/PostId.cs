using Domain.Common;

namespace Domain.Posts;

public sealed class PostId : ValueObject
{
    public PostId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PostId New()
    {
        return new PostId(Guid.NewGuid());
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