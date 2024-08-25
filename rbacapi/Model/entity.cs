using System.Text.Json.Serialization;

namespace rbacapi.Model
{
    public class Entity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ClientId { get; set; }
    }
}
