using TBC.BooksCatalog.Write.Domain.Aggregates;

namespace TBC.BooksCatalog.Write.Domain.UnitTests;

public class BookAggregateTests
{
    [Fact]
    public void GivenValidParamaters_WhenCrateIsInvoked_ThenItShouldReturnABook()
    {
        //Arrange
        var name = Name.Create("Tales Of Beedle the Bard.").Value!;
        var description = Description.Create("The ultimate stories told by beedle the bard.").Value!;
        var publisher = Publisher.Create("Maddog Publishers").Value!;
        List<AuthorId> authors =
            [AuthorId.Create(Guid.CreateVersion7()).Value!, AuthorId.Create(Guid.CreateVersion7()).Value!];

        List<GenreId> genres = [GenreId.Create(Guid.CreateVersion7()).Value!];

        var todayDateUtc = DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().DateTime);
        var publicationDateValue = new DateOnly(1992, 12, 31);
        var publicationDate = PublicationDate.Create(publicationDateValue, todayDateUtc).Value!;

        //Act
        var bookCreateResult = Book.Create(name, description, publisher, publicationDate, authors, genres);
        var book = bookCreateResult.Value!;

        //Assert
        Assert.True(bookCreateResult.IsSuccess);
        Assert.NotNull(book.Id);
        Assert.Equal(7, book.Id.Value.Version);
        Assert.Equal(name, book.Title);
        Assert.Equal(description, book.Description);
        Assert.Equal(publisher, book.Publisher);
        Assert.Equal(publicationDate, book.PublicationDate);
        Assert.Equal(authors, book.Authors);
        Assert.Equal(genres, book.Genres);
    }

    [Fact]
    public void Given_NoAuthorsOrGenres_When_CreateIsInvoked_Then_ErrorsShouldBeReturned()
    {
        //Arrange
        var name = Name.Create("Tales Of Beedle the Bard.").Value!;
        var description = Description.Create("The ultimate stories told by beedle the bard.").Value!;
        var publisher = Publisher.Create("Maddog Publishers").Value!;
        var todayDateUtc = DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().DateTime);
        var publicationDateValue = new DateOnly(1992, 12, 31);
        var publicationDate = PublicationDate.Create(publicationDateValue, todayDateUtc).Value!;

        IEnumerable<Error> expectedErrors = [Errors.NoAuthorsProvided, Errors.NoGenresProvided];


        //Act
        var bookCreateResult = Book.Create(name, description, publisher, publicationDate, [], []);

        //Assert
        Assert.False(bookCreateResult.IsSuccess);
        Assert.Equal(expectedErrors, bookCreateResult.Errors!.AsEnumerable());
    }
}
