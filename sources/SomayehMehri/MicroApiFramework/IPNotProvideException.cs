internal class IPNotProvideException : Exception
{
    public IPNotProvideException()
    {
    }

    public IPNotProvideException(string? message) : base(message)
    {
    }
}