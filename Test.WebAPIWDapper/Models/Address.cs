namespace WebAPIWDapper.Models;
using System.Text.Json.Serialization;
public class Address
{
    [JsonIgnore] 
    public int Id { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }

    
    [JsonIgnore] 
    public int? EmployeeId { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime UpdatedDate { get; set; }
}
