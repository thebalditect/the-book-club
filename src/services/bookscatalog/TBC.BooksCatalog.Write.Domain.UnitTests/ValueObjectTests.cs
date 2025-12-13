using TBC.BooksCatalog.Write.Domain.Aggregates;

namespace TBC.BooksCatalog.Write.Domain.UnitTests;

public class ValueObjectTests
{
    [Fact]
    public void Given_ValidValue_When_CreateIsInvoked_OnName_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var name = "Tales of Sinmara and Sindbad";

        //Act
        var result = Name.Create(name);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(name, result.Value?.Value);
    }

    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Given_InvalidValue_When_CreateIsInvoked_OnName_Then_Errors_ShouldBeReturned(string name)
    {
        //Arrange
        IEnumerable<Error> expectedErrors = [Errors.InvalidName];

        //Act
        var result = Name.Create(name);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }

    [Fact]
    public void Given_ValidValue_When_CreateIsInvoked_OnDescription_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var description =
            "Tales of Sinmara and Sindbad is a wonderful book which explores the adventures of sinmara and sindbad.";

        //Act
        var result = Description.Create(description);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(description, result.Value?.Value);
    }

    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Given_InvalidValue_When_CreateIsInvoked_OnDescription_Then_Errors_ShouldBeReturned(string name)
    {
        //Arrange
        IEnumerable<Error> expectedErrors = [Errors.InvalidDescription];

        //Act
        var result = Description.Create(name);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }

    [Fact]
    public void Given_ValidValue_When_CreateIsInvoked_OnPublisher_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var publisher = "Maddog Publications";

        //Act
        var result = Publisher.Create(publisher);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(publisher, result.Value?.Value);
    }

    [InlineData("")]
    [InlineData(" ")]
    [Theory]
    public void Given_InvalidValue_When_CreateIsInvoked_OnPublisher_Then_Errors_ShouldBeReturned(string name)
    {
        //Arrange
        IEnumerable<Error> expectedErrors = [Errors.InvalidPublisher];

        //Act
        var result = Publisher.Create(name);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }

    [Fact]
    public void Given_ValidValue_When_CreateIsInvoked_OnPublicationDate_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var publicationDate = new DateOnly(1997, 12, 12);
        var todayDateUtc = DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().DateTime);

        //Act
        var result = PublicationDate.Create(publicationDate, todayDateUtc);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(publicationDate, result.Value?.Value);
    }

    [Fact]
    public void Given_FutureDate_When_CreateIsInvoked_OnPublicationDate_Then_Errors_ShouldBeReturned()
    {
        //Arrange
        var futureDate = new DateOnly(2303, 1, 12);
        var todayDateUtc = DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().DateTime);
        IEnumerable<Error> expectedErrors = [Errors.PublicationDateInFuture];

        //Act
        var result = PublicationDate.Create(futureDate, todayDateUtc);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }

    [Fact]
    public void GivenAValidV7Guid_When_CreateIsInvoked_OnAuthorId_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var guid = Guid.CreateVersion7();

        //Act
        var result = AuthorId.Create(guid);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(guid, result.Value!.Value);
    }

    [Fact]
    public void GivenInvalidGuid_When_CreateIsInvoked_OnAuthorId_Then_Errors_ShouldBeReturned()
    {
        //Arrange
        var guid = Guid.NewGuid();
        IEnumerable<Error> expectedErrors = [Errors.InvalidAuthorId];

        //Act
        var result = AuthorId.Create(guid);
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }

    [Fact]
    public void GivenAValidV7Guid_When_CreateIsInvoked_OnGenreId_Then_Instance_ShouldBeReturned()
    {
        //Arrange
        var guid = Guid.CreateVersion7();

        //Act
        var result = GenreId.Create(guid);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(guid, result.Value?.Value);
    }

    [Fact]
    public void GivenInvalidGuid_When_CreateIsInvoked_OnGenreId_Then_Errors_ShouldBeReturned()
    {
        //Arrange
        var guid = Guid.NewGuid();
        IEnumerable<Error> expectedErrors = [Errors.InvalidGenreId];

        //Act
        var result = GenreId.Create(guid);
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrors, result.Errors);
    }
}
