using TBC.Kernel.Domain;

namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record Description
{
    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<Description> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Description>([Errors.InvalidDescription]);
        }

        return Result.Success(new Description(description));
    }
}
