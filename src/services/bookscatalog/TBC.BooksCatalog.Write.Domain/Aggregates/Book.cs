namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public sealed class Book : EntityBase
{
    private Book(Name name, Description description, Publisher publisher, PublicationDate publicationDate,
        IEnumerable<AuthorId> authors, IEnumerable<GenreId> genres)
    {
        Id = new BookId();
        Title = name;
        Description = description;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Authors = authors;
        Genres = genres;
    }

    public BookId Id { get; private set; }
    public Name Title { get; private set; }
    public Description Description { get; private set; }
    public Publisher Publisher { get; private set; }
    public PublicationDate PublicationDate { get; private set; }
    public IEnumerable<AuthorId> Authors { get; private set; }
    public IEnumerable<GenreId> Genres { get; private set; }

    public static Result<Book> Create(Name name, Description description, Publisher publisher,
        PublicationDate publicationDate, IEnumerable<AuthorId> authors, IEnumerable<GenreId> genres)
    {
        var authorIds = authors.ToList();
        var genreIds = genres.ToList();

        List<Error> errors = [];

        if (authorIds.Count == 0)
        {
            errors.Add(Errors.NoAuthorsProvided);
        }

        if (genreIds.Count == 0)
        {
            errors.Add(Errors.NoGenresProvided);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<Book>(errors);
        }

        Book book = new(name, description, publisher, publicationDate, authorIds, genreIds);
        return Result.Success(book);
    }
}
