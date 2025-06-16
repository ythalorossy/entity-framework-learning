using Domain.Blogs;
using Domain.Common;

namespace Domain.Categories;

public sealed class CategoryId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static CategoryId New()
    {
        return new CategoryId(Guid.NewGuid());
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