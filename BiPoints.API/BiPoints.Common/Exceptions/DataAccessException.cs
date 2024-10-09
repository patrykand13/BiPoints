namespace BiPoints.Common.Exceptions
{
    public class DataAccessException : Exception
    {
        public string Path { get; }
        public DataAccessException(string message, string path = null, Exception innerException = null)
            : base(message, innerException)
        {
            Path = path;
        }
    }
}
