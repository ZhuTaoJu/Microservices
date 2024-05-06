namespace Email.API.Infrastructure.Exceptions
{
    public class EmailDomainException : Exception
    {
        public EmailDomainException()
        { }

        public EmailDomainException(string message)
            : base(message)
        { }

        public EmailDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
