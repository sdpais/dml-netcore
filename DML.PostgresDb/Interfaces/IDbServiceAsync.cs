namespace DML.PostgresDb.Interfaces;

public interface IDbServiceAsync
{

    //Task<T> GetAsync<T>(string command, object parms); 
    Task<T?> GetFirst<T>(string command, object parms) where T : class;
    Task<T?> GetFirst<T>(string command);
    Task<List<T>> GetMany<T>(string command, object parms);
    Task<List<T>> GetMany<T>(string command);
    Task<int> EditData(string command, object parms);
    Task<int> EditData(string command);

    Task<int> ExecuteScalar(string command);
    Task<int> ExecuteScalar(string command, object parms);


}