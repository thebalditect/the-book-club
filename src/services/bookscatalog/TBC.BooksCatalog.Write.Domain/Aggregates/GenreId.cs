namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record GenreId
{
    private const int AllowedVersion = 7;

    private GenreId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static Result<GenreId> Create(Guid genreId)
    {
        if (genreId.Version != AllowedVersion)
        {
            return Result.Failure<GenreId>([Errors.InvalidGenreId]);
        }

        return Result.Success(new GenreId(genreId));
    }
}
