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
                "INSERT INTO public.login (username, password) VALUES (@username, @password)",
                login);
        return true;
    }

    public async Task<List<Login>> GetLoginList()
    {
        var loginList = await _dbService.GetAll<Login>("SELECT * FROM public.login", new { });
        return loginList;
    }

    public async Task<List<Login>> GetLogins(string userName)
    {
        List<Login> loginList = await _dbService.GetAll<Login>("SELECT * FROM public.login where username=@username", new { userName });
        return loginList;
    }
    public async Task<Login> GetLogin(string userName)
    {
        Login login = await _dbService.GetAsync<Login>("SELECT TOP 1 * FROM public.login where username=@username", new {userName});
        return login;
    }

    public async Task<Login> UpdateLogin(Login login)
    {
        var updateLogin =
            await _dbService.EditData(
                "Update public.login SET password=@password WHERE name=@userName",
                login);
        return login;
    }

    public async Task<bool> DeleteLogin(string userName)
    {
        var deleteLogin = await _dbService.EditData("DELETE FROM public.login WHERE username=@username", new { userName });
        return true;
    }
}