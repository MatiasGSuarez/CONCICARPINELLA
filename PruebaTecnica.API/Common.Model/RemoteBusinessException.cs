using System.Web;


namespace PruebaTecnica.API.Common.Model
{
    [Serializable]
    public class RemoteBusinessException : Exception
    {
        public string CodeError { get; set; }
        public RemoteBusinessException(string message) : base(message) { }

        public RemoteBusinessException(string message, string codeError) : base(message)
        {
            CodeError = codeError;
        }

        public RemoteBusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
