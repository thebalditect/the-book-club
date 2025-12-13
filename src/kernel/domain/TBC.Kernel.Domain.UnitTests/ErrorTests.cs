namespace TBC.Kernel.Domain.UnitTests;

public class ErrorTests
{
    [Theory]
    [InlineData("UnitTest.Error", "This is a test description")]
    [InlineData("BooksCatalog.Error", "Search operation failed.")]
    public void Given_Error_Code_And_Description_Failure_Should_Return_Failure_Type_Error(string code,
        string description)
    {
        //Arrange
        var expectedErrorType = ErrorType.Failure;
        //Act
        var error = Error.Failure(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(expectedErrorType, error.ErrorType);
    }

    [Theory]
    [InlineData("UnitTest.Error", "Test with id 1 was not found.")]
    [InlineData("BooksCatalog.Error", "Book with id 2 was not found.")]
    public void Given_Error_Code_And_Description_NotFound_Should_Return_NotFound_Type_Error(string code,
        string description)
    {
        //Arrange
        var expectedErrorType = ErrorType.NotFound;

        //Act
        var error = Error.NotFound(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(expectedErrorType, error.ErrorType);
    }

    [Theory]
    [InlineData("UnitTest.Error", "Test with id 1 already exists.")]
    [InlineData("BooksCatalog.Error", "Book with id 2 already exists.")]
    public void Given_Error_Code_And_Description_Conflict_Should_Return_Conflict_Type_Error(string code,
        string description)
    {
        //Arrange
        var expectedErrorType = ErrorType.Conflict;

        //Act
        var error = Error.Conflict(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(expectedErrorType, error.ErrorType);
    }

    [Theory]
    [InlineData("UnitTest.Error", "Name can not be empty.")]
    [InlineData("BooksCatalog.Error", "Description can not be empty.")]
    public void Given_Error_Code_And_Description_Validation_Should_Return_Validation_Type_Error(string code,
        string description)
    {
        //Arrange
        var expectedErrorType = ErrorType.Validation;

        //Act
        var error = Error.Validation(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(expectedErrorType, error.ErrorType);
    }
}
