using System.Text.Json.Serialization;

namespace DML.Domain.Entities;

public class Employee
{
    [JsonIgnore]
    public int Id { get; set; }
    //what is 
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    [JsonIgnore]
    public string? DateOfBirthEncrypted { get; set; }
    public required string MobileNumber { get; set; }

    [JsonIgnore]
    public int EncryptionKeyId { get; set; }
    public string SSN { get; set; }

    [JsonIgnore]
    public string? SSNEncrypted { get; set; }
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime UpdatedDate { get; set; }
}