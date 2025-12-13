namespace TBC.BooksCatalog.Write.Domain.Aggregates;

public static class Errors
{
    private const string DomainValidationError = "BooksCatalog.Write.Domain.ValidationError";
    public static Error InvalidName => Error.Validation(DomainValidationError, "Name can not be empty or whitespace");

    public static Error InvalidDescription =>
        Error.Validation(DomainValidationError, "Description can not be empty or whitespace");

    public static Error InvalidPublisher =>
        Error.Validation(DomainValidationError, "Publisher name can not be empty or whitespace");

    public static Error PublicationDateInFuture =>
        Error.Validation(DomainValidationError, "PublicationDate can not be in the future");

    public static Error InvalidAuthorId =>
        Error.Validation(DomainValidationError, "AuthorId should be a valid v7 uuid.");

    public static Error NoAuthorsProvided =>
        Error.Validation(DomainValidationError, "A book should have at least one author.");

    public static Error InvalidGenreId =>
        Error.Validation(DomainValidationError, "GenreId should be a valid v7 uuid.");

    public static Error NoGenresProvided =>
        Error.Validation(DomainValidationError, "A book should have at least one genre.");
}
