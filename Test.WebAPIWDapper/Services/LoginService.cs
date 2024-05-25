using WebAPIWDapper.Models;



namespace WebAPIWDapper.Services;

public class LoginService : ILoginService
{
    private readonly IDbService _dbService;

    public LoginService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<bool> CreateLogin(Login login)
    {
        var result =
            await _dbService.EditData(
                "INSERT INTO public.logins (username, password, EncryptionKeyId) VALUES (@username, @password, @EncryptionKeyId)",
                login);
        return true;
    }

    public async Task<List<Login>> GetLoginList()
    {
        var loginList = await _dbService.GetAll<Login>("SELECT * FROM public.logins", new { });
        return loginList;
    }
    public async Task<List<Login>> GetLogins(string userName)
    {
        List<Login> logins = await _dbService.GetAll<Login>("SELECT TOP 1 * FROM public.logins where username=@username", new { userName });
        return logins;
    }
    public async Task<Login?> GetLogin(string userName)
    {
        Login? login = await _dbService.GetAsync<Login>("SELECT TOP 1 * FROM public.logins where username=@username", new { userName });
        return login;
    }
  

    public async Task<Login> UpdateLogin(Login login)
    {
        var updateLogin =
            await _dbService.EditData(
                "Update public.logins SET password=@password, name=@userName, EncryptionKeyId=@encryptionKeyId, EmployeeId=@employeeId WHERE Id=@Id",
                login);
        return login;
    }

    public async Task<bool> DeleteLogin(string userName)
    {
        var deleteLogin = await _dbService.EditData("DELETE FROM public.logins WHERE username=@username", new { userName });
        return true;
    }
}