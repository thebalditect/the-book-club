using TBC.Kernel.Domain;

namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record Name
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static Result<Name> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Name>([Errors.InvalidName]);
        }

        return Result.Success(new Name(name));
    }
}
