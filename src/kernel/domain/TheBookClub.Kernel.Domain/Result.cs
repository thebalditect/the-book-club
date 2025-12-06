namespace TheBookClub.Kernel.Domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error>? Errors { get; private set; }

    protected Result(bool isSuccess, List<Error>? errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(List<Error> errors)
    {
        return errors.Count == 0
            ? throw new InvalidOperationException("Failure result should contain at least one error.")
            : new Result(false, errors);
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value, true, null);
    }

    public static Result<T> Failure<T>(List<Error> errors)
    {
        return errors.Count == 0
            ? throw new InvalidOperationException("Failure result should contain at least one error.")
            : new Result<T>(default, false, errors);
    }
}

public class Result<T> : Result
{
#pragma warning disable IDE0032
    private readonly T? _value;
#pragma warning restore IDE0032

    public T? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The property named Value can not be accessed for failure result.");

    public Result(T? value, bool isSuccess, List<Error>? errors) : base(isSuccess, errors)
    {
        _value = value;
    }
}
