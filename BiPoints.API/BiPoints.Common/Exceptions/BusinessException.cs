namespace BiPoints.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public string Path { get; }
        public BusinessException(string message, string path = null, Exception innerException = null)
            : base(message, innerException)
        {
            Path = path;
        }
    }
}
