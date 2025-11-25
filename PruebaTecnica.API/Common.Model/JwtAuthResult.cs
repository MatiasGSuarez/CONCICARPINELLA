using System.Text.Json.Serialization;
using System.Web;

namespace PruebaTecnica.API.Common.Model
{
    public class JwtAuthResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public RefreshToken RefreshToken { get; set; }
    }

}