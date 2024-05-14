
using System.Data;
using Dapper;
using Npgsql;


namespace WebAPIWDapper.Services;


public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("Employeedb"));
    }

    //public async Task<T> GetAsync<T>(string command, object parms)
    //{
    //    T result;

    //    result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

    //    return result;

    //}
    public async Task<T?> GetAsync<T>(string command, object parms) where T : class
    {
        IEnumerable<T> result;

        result = await _db.QueryAsync<T>(command, parms).ConfigureAwait(false);

        return result.FirstOrDefault();
    }


    public async Task<List<T>> GetAll<T>(string command, object parms)
    {

        List<T> result = new List<T>();

        result = (await _db.QueryAsync<T>(command, parms)).ToList();

        return result;
    }
    public async Task<List<T>> GetAll<T>(string command)
    {

        List<T> result = new List<T>();

        result = (await _db.QueryAsync<T>(command)).ToList();

        return result;
    }
    public async Task<int> EditData(string command, object parms)
    {
        int result;

        result = await _db.ExecuteAsync(command, parms);

        return result;
    }
    public async Task<int> EditData(string command)
    {
        int result;

        result = await _db.ExecuteAsync(command);

        return result;
    }
}