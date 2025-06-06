using Domain.Common;

namespace Domain.Blogs;

public sealed class BlogId : ValueObject
{
    public BlogId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BlogId New()
    {
        return new BlogId(Guid.NewGuid());
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