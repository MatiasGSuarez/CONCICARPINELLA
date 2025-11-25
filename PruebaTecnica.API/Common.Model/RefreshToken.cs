using System.Text.Json.Serialization;
using System.Web;



namespace PruebaTecnica.API.Common.Model
{
    public class RefreshToken
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }
        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
}