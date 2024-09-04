using System.Text.Json.Serialization;

namespace DML.Domain.Entities;

public class Entity
{
   
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
