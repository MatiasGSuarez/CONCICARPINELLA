namespace PruebaTecnica.API.Common
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string CodeError { get; set; }
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, string codeError) : base(message)
        {
            this.CodeError = codeError;
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
