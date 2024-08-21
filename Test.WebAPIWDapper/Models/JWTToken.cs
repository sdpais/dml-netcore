using System.Text.Json.Serialization;
namespace WebAPIWDapper.Models
{
    public class JWTToken
    {
        public string ? AccessToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public string ? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }
    }
}
