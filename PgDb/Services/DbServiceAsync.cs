
using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;


namespace DML.PostgresDb.Services;


public class DbServiceAsync : IDbServiceAsync
{
    private readonly IDbConnection _db;

    public DbServiceAsync(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("RBACdb"));
    }

    //public async Task<T> GetAsync<T>(string command, object parms)
    //{
    //    T result;

    //    result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();

    //    return result;

    //}
   
    public async Task<T?> GetFirst<T>(string command, object parms) where T : class
    {
        IEnumerable<T> result;

        result = await _db.QueryAsync<T>(command, parms).ConfigureAwait(false);

        return result.FirstOrDefault();
    }

    public async Task<T?> GetFirst<T>(string command) 
    {
        IEnumerable<T> result;

        result = await _db.QueryAsync<T>(command).ConfigureAwait(false);

        return result.FirstOrDefault(); ; 
    }
    public async Task<List<T>> GetMany<T>(string command, object parms)
    {

        List<T> result = new List<T>();

        result = (await _db.QueryAsync<T>(command, parms)).ToList();

        return result;
    }
    public async Task<List<T>> GetMany<T>(string command)
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

    public async Task<int> ExecuteScalar(string command)
    {

        return await _db.ExecuteScalarAsync<int>(command);

    }

    public async Task<int> ExecuteScalar(string command, object parms)
    {
        int result;
        
        result = await _db.ExecuteScalarAsync<int>(command, parms);

        return result;

    }
}