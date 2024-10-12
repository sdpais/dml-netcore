//using MongoDB.Driver;
//using System.Data;
//using DML.ConfigServices.Interfaces;
//using Microsoft.Extensions.Configuration;
//using DML.MongoDb.Interfaces;
//using DML.ConfigServices.Services;


////https://medium.com/@pererikbergman/repository-design-pattern-e28c0f3e4a30
////https://medium.com/@jaydeepvpatil225/mongodb-basics-and-crud-operation-using-net-core-7-web-api-884b5b76549a
////https://medium.com/@leonardomartins_27620/connecting-to-a-mongodb-database-in-c-with-net-core-e34d470b3e72

//namespace DML.MongoDb.Services;


//public class MDbServiceAsync : IMDbServiceAsync
//{
//    private readonly IMongoDbSettings _db;
//    private readonly MongoClient _mongoClient;
//    private readonly IMongoDatabase _mongoDatabase;
    

//    public MDbServiceAsync(IConfiguration configuration)
//    {
//        _db = new MongoDbSettings(configuration);
//        _mongoClient = new MongoClient(_db.ConnectionString);
//        var _mongoDatabase = _mongoClient.GetDatabase(_db.DatabaseName);
//    }

//    public async Task<List<T>> ListAsync()
//    {
//        var productCollection = _mongoDatabase.GetCollection<T>(_db.CollectionName);
//        return await productCollection.Find(_ => true).ToListAsync();
//    }
//}