namespace TBC.Kernel.Domain.UnitTests;

public class ResultTests
{
    [Fact]
    public void Given_Operation_Success_When_Success_Is_Executed_Then_SuccessResult_Is_Returned()
    {
        //Act
        var result = Result.Success();

        //Act
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Errors);
    }

    [Fact]
    public void Given_Operation_Failure_When_Failure_Is_Executed_Then_FailureResult_Is_Returned()
    {
        //Arrange
        List<Error> errors = [Error.Failure("BookCatalog.Failure", "Operation failed.")];

        //Act
        var result = Result.Failure(errors);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Equal(errors, result.Errors);
    }

    [Fact]
    public void Given_Operation_Success_When_Object_Is_Passed_Then_SuccessResult_With_Object_Is_Returned()
    {
        //Arrange
        var dummyReturnValue = new DummyReturnValue("returnValue");

        //Act
        var result = Result.Success(dummyReturnValue);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Errors);
        Assert.NotNull(result.Value);
        Assert.Equal(dummyReturnValue, result.Value);
    }

    [Fact]
    public void Given_Operation_Failure_When_Typed_Failure_Is_Invoked_Then_ErrorResult_Is_Returned()
    {
        //Arrange
        List<Error> errors = [Error.Failure("BookCatalog.Failure", "Operation failed.")];

        //Act
        var result = Result.Failure<DummyReturnValue>(errors);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Equal(errors, result.Errors);
    }

    [Fact]
    public void Given_Operation_Failure_When_Result_Value_Is_Accessed_Then_InvalidOperationException_Is_Thrown()
    {
        //Arrange
        List<Error> errors = [Error.Failure("BookCatalog.Failure", "Operation failed.")];
        string expectedErrorMessage = "The property named Value can not be accessed for failure result.";

        //Act
        var result = Result.Failure<DummyReturnValue>(errors);

        Func<DummyReturnValue> func = () => result.Value;
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(func);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Given_Operation_Failure_When_No_Error_Is_Provided_Then_InvalidOperationException_Is_Thrown()
    {
        //Arrange
        string expectedErrorMessage = "Failure result should contain at least one error.";

        //Assert
        InvalidOperationException exceptionOnBaseMethodInvocation =
            Assert.Throws<InvalidOperationException>(() => Result.Failure([]));
        Assert.Equal(expectedErrorMessage, exceptionOnBaseMethodInvocation.Message);

        InvalidOperationException exceptionOnDerivedMethodInvocation =
            Assert.Throws<InvalidOperationException>(() => Result.Failure<DummyReturnValue>([]));
        Assert.Equal(expectedErrorMessage, exceptionOnDerivedMethodInvocation.Message);
    }
}
