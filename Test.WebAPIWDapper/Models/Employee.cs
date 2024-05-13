namespace WebAPIWDapper.Models;

public class Employee
{
    public int Id { get; set; }
    //what is 
    public required string Name { get; set; }
    public int Age { get; set; }
    public required string Address { get; set; }
    public required string MobileNumber { get; set; }
    public int EncryptionKeyId { get; set; }
    public string SSNEncrypted { get; set; }
}