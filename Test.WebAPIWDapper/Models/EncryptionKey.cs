using System.Text.Json.Serialization;
namespace WebAPIWDapper.Models;
public class EncryptionKey
{
    public int Id { get; set; }
    public string? KeyString { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? ReplacedById { get; set; }
    

}
