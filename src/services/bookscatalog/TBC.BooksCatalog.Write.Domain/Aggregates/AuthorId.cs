namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record AuthorId
{
    private const int AllowedVersion = 7;

    private AuthorId(Guid guid)
    {
        Value = guid;
    }

    public Guid Value { get; private set; }

    public static Result<AuthorId> Create(Guid authorId)
    {
        if (authorId.Version != AllowedVersion)
        {
            return Result.Failure<AuthorId>([Errors.InvalidAuthorId]);
        }

        return Result.Success(new AuthorId(authorId));
    }
}
