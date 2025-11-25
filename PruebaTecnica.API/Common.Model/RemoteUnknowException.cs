using System.Web;


namespace PruebaTecnica.API.Common.Model
{
    [Serializable]
    public class RemoteUnknownException : Exception
    {
        public string CodeError { get; set; }
        public string BaseAddress { get; set; }

        public string ExternalLogId { get; set; }

        public RemoteUnknownException(string message) : base(message) { }

        public RemoteUnknownException(string message, string codeError) : base(message)
        {
            CodeError = codeError;
        }

        public RemoteUnknownException(string message, string baseAddress, string externalLogId) : base(message)
        {
            BaseAddress = baseAddress;
            ExternalLogId = externalLogId;
        }
    }
}