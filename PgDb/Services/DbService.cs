
using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;


namespace DML.PostgresDb.Services;


public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("RBACdb"));
    }


    #region Methods
    public List<T> GetAll<T>(string command, object parms)
    {

        List<T> result = new List<T>();

        result = _db.Query<T>(command, parms).ToList();

        return result;
    }
    public List<T> GetAll<T>(string command)
    {

        List<T> result = new List<T>();

        result = _db.Query<T>(command).ToList();

        return result;
    }
    public  int EditData(string command, object parms)
    {
        int result;

        result =  _db.Execute(command, parms);

        return result;
    }
    public  int EditData(string command)
    {
        int result;

        result =  _db.Execute(command);

        return result;
    }
    #endregion
}