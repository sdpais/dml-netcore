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
    public string ConnectionString {
        get {
            var mongoHost = ApplicationConfiguration.GetSetting("MongoDb:Server");
            var mongoPort = ApplicationConfiguration.GetSetting("MongoDb:Port");
            var mongoUsername = ApplicationConfiguration.GetSetting("MongoDb:Username");
            var mongoPassword = (ApplicationConfiguration.GetSetting("MongoDb:Password"));
            var mongoSSLEnabled = (ApplicationConfiguration.GetSetting("MongoDb:SSL"));
            return $"mongodb://{mongoUsername}:{mongoPassword}@{mongoHost}:{mongoPort}/?ssl={mongoSSLEnabled}";
        }
    }
    public string DatabaseName {
        get
        {
            var mongoDatabase = ApplicationConfiguration.GetSetting("MongoDb:DatabaseName");
            return mongoDatabase;
        }
    }
    public string CollectionName
    {
        get
        {
            var mongoCollectionName = ApplicationConfiguration.GetSetting("MongoDb:CollectionName");
            return mongoCollectionName;
        }
    }    
}