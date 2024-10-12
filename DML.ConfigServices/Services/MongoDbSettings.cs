using DML.ConfigServices.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DML.ConfigServices.Services;

public class MongoDbSettings : IMongoDbSettings
{
    private IConfiguration _configuration { get; }
    public MongoDbSettings(IConfiguration configuration) {
        _configuration = configuration;
    }
    public string ConnectionString {
        get {
            if (_configuration is null)
            {
                throw new ArgumentNullException(nameof(_configuration));
            }
            var mongoHost = _configuration.GetValue<string>("MongoDb:Server");
            var mongoPort = _configuration.GetValue<string>("MongoDb:Port");
            var mongoUsername = _configuration.GetValue<string>("MongoDb:Username");
            var mongoPassword = (_configuration.GetValue<string>("MongoDb:Password"));
            var mongoSSLEnabled = (_configuration.GetValue<string>("MongoDb:SSL"));
            return $"mongodb://{mongoUsername}:{mongoPassword}@{mongoHost}:{mongoPort}/?ssl={mongoSSLEnabled}";
        }
    }
    public string DatabaseName {
        get
        {
            if (_configuration is null)
            {
                throw new ArgumentNullException(nameof(_configuration));
            }
            var mongoDatabase = _configuration.GetValue<string>("MongoDb:DatabaseName");
            return mongoDatabase;
        }
    }
    public string CollectionName
    {
        get
        {
            if (_configuration is null)
            {
                throw new ArgumentNullException(nameof(_configuration));
            }
            var mongoCollectionName = _configuration.GetValue<string>("MongoDb:CollectionName");
            return mongoCollectionName;
        }
    }    
}