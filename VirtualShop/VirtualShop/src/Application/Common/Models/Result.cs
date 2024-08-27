
namespace VirtualShop.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }
    public object? SuccessStatus { get; set; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    internal static Result Success(object status)
    {
        var result = new Result(true, Array.Empty<string>());
        result.SuccessStatus = status;
        return result;
    }
}
