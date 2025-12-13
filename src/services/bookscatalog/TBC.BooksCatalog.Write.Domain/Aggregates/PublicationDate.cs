namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed record PublicationDate
{
    private PublicationDate(DateOnly publicationDate)
    {
        Value = publicationDate;
    }

    public DateOnly Value { get; private set; }

    public static Result<PublicationDate> Create(DateOnly publicationDate, DateOnly todayDateUtc)
    {
        if (publicationDate > todayDateUtc)
        {
            return Result.Failure<PublicationDate>([Errors.PublicationDateInFuture]);
        }

        return Result.Success(new PublicationDate(publicationDate));
    }
}
