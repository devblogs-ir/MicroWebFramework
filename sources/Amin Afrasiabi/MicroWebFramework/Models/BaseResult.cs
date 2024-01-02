namespace MicroWebFramework.Models;

public class BaseResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
}

public class BaseResult<TResultType> : BaseResult
{
    public TResultType? Result { get; set; }
}
