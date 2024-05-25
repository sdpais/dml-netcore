namespace WebAPIWDapper.Models;

public class Employee
{
    public int Id { get; set; }
    //what is 
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string DateOfBirthEncrypted { get; set; }
    public required string MobileNumber { get; set; }
    public int EncryptionKeyId { get; set; }
    public string SSN { get; set; }
    public string SSNEncrypted { get; set; }
    public DateTime CreatedDate { get; set; }
}