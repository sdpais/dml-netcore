﻿namespace WebAPIWDapper.Models;
public class Login
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int EncryptionKeyId { get; set; }
    public int EmployeeId { get; set; }
}
