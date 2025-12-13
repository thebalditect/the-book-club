namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record Publisher
{
    private Publisher(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<Publisher> Create(string publisher)
    {
        if (string.IsNullOrWhiteSpace(publisher))
        {
            return Result.Failure<Publisher>([Errors.InvalidPublisher]);
        }

        return Result.Success(new Publisher(publisher));
    }
}
