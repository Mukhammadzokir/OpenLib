namespace OpenLibrary.Service.Exceptions;

public class LibraryException : Exception
{
    public int statusCode;

    public LibraryException(int Code, string Message) : base(Message)
    {
        statusCode = Code;
    }
}
