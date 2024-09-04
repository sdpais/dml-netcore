using MongoDB.Driver;
using System.Data;
using DML.ConfigServices.MongoDb;
using Microsoft.Extensions.Configuration;

//https://medium.com/@pererikbergman/repository-design-pattern-e28c0f3e4a30
//https://medium.com/@jaydeepvpatil225/mongodb-basics-and-crud-operation-using-net-core-7-web-api-884b5b76549a
//https://medium.com/@leonardomartins_27620/connecting-to-a-mongodb-database-in-c-with-net-core-e34d470b3e72

namespace DML.MongoDb.Services;


public class MDbServiceAsync : IMDbServiceAsync
{
    private readonly IEntityDbSettings _db;

    public MDbServiceAsync(IConfiguration configuration)
    {
        _db.ConnectionString = configuration.GetConnectionString("MongoEntityDbCS");
        _db.DatabaseName = configuration.GetConnectionString("MongoEntityDbName");
        _db.DatabaseName = configuration.GetConnectionString("MongoEntityDbName");
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
        //todo
        result = new List<T>();
        //result = await _db.QueryAsync<T>(command, parms).ConfigureAwait(false);

        return result.FirstOrDefault();
    }

    public async Task<T?> GetFirst<T>(string command) 
    {
        IEnumerable<T> result;
        //todo
        result = new List<T>();
        //result = await _db.QueryAsync<T>(command).ConfigureAwait(false);

        return result.FirstOrDefault(); ; 
    }
    public async Task<List<T>> GetMany<T>(string command, object parms)
    {

        List<T> result = new List<T>();
        //todo
        //result = (await _db.QueryAsync<T>(command, parms)).ToList();

        return result;
    }
    public async Task<List<T>> GetMany<T>(string command)
    {

        List<T> result = new List<T>();
        //todo
        //result = (await _db.QueryAsync<T>(command)).ToList();

        return result;
    }
    public async Task<int> EditData(string command, object parms)
    {
        int result;
        //todo
        result = 0;
        //result = await _db.ExecuteAsync(command, parms);

        return result;
    }
    public async Task<int> EditData(string command)
    {
        int result;

        //todo
        result = 0;
        //result = await _db.ExecuteAsync(command);

        return result;
    }

    public async Task<int> ExecuteScalar(string command)
    {
        //todo
        //return await _db.ExecuteScalarAsync<int>(command);
        return 0;

    }

    public async Task<int> ExecuteScalar(string command, object parms)
    {
        int result;

        //todo
        result = 0;
        //result = await _db.ExecuteScalarAsync<int>(command, parms);

        return result;

    }
}