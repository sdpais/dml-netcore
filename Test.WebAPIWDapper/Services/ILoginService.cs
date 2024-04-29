using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public interface ILoginService
{
    Task<bool> CreateLogin(Login login);
    Task<List<Login>> GetLogins(string userName);
    Task<Login> GetLogin(string userName);
    Task<List<Login>> GetLoginList();
    Task<Login> UpdateLogin(Login login);
    Task<bool> DeleteLogin(string userName);
    
}