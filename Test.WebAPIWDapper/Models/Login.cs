﻿namespace WebAPIWDapper.Models;
using System.Text.Json.Serialization;
public class Login
{
    [JsonIgnore] 
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    
    [JsonIgnore] 
    public int EncryptionKeyId { get; set; }
    
    [JsonIgnore] 
    public int? EmployeeId { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime UpdatedDate { get; set; }
}
