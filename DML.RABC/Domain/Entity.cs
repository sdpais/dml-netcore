using System.Text.Json.Serialization;

namespace DML.RBAC.Domain;

public class Entity
{
    [JsonIgnore]
    public int Id { get; set; }
    public required string EntityName { get; set; }
    public int ClientId { get; set; }
    public int ModuleId { get; set; }
    public bool IsActive { get; set; }

    [JsonIgnore]
    public DateTime CreatedOn { get; set; }
    [JsonIgnore]
    public DateTime UpdatedOn { get; set; }
}
