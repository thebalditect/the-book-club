namespace TBC.Kernel.Domain;

public class Result
{
    private readonly List<Error>? _errors;


    protected Result(bool isSuccess, IEnumerable<Error>? errors)
    {
        IsSuccess = isSuccess;
        _errors = errors?.ToList();
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyCollection<Error>? Errors => _errors?.AsReadOnly();

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value, true, null);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        var errorList = errors.ToList();

        return errorList.Count == 0
            ? throw new InvalidOperationException("Failure result should contain at least one error.")
            : new Result(false, errorList);
    }

    public static Result<T> Failure<T>(IEnumerable<Error> errors)
    {
        var errorList = errors.ToList();
        return errorList.Count == 0
            ? throw new InvalidOperationException("Failure result should contain at least one error.")
            : new Result<T>(default, false, errorList);
    }
}

public class Result<T> : Result
{
#pragma warning disable IDE0032
    private readonly T? _value;
#pragma warning restore IDE0032

    public Result(T? value, bool isSuccess, IEnumerable<Error>? errors) : base(isSuccess, errors)
    {
        _value = value;
    }

    public T? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The property named Value can not be accessed for failure result.");
}
