using System.Text.Json.Serialization;


namespace DML.Domain.Entities;

public class Client
{
    [JsonIgnore]
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive{ get; set; }

    [JsonIgnore]
    public DateTime CreatedOn { get; set; }
    [JsonIgnore]
    public DateTime UpdatedOn { get; set; }
}

