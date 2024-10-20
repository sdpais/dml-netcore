using StackExchange.Redis;
using System.Net;
using DML.ConfigServices.Services;
using DML.RedisCache.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DML.RedisCache.Services;

public class RedisDbProvider : IRedisDbProvider
{
    private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
    private bool disposed = false;
    private readonly string _connectionString;
    private WebAPIConfig _webAPIConfig;
    public RedisDbProvider()
    {
        _webAPIConfig = new WebAPIConfig();

        _connectionString = _webAPIConfig.RedisConnectionString;
        _connectionString = _connectionString ?? throw new ArgumentNullException(_connectionString);
        ConfigurationOptions redisConfigOptions = ConfigurationOptions.Parse("192.168.2.41:6381");
        //ConfigurationOptions redisConfigOptions = ConfigurationOptions.Parse("192.168.2.40:6380,192.168.2.41:6380");
        redisConfigOptions.Ssl = false;
        //              redisConfigOptions.Password = WebUtility.UrlEncode("ThisSecretNeedstobebreaterthan256charactersinorderforittobeavalidkeyiamtr1@3%67Hd*@thSzxczdcsddsdsad");
        redisConfigOptions.AbortOnConnectFail = false;
        //              _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_connectionString));
        _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConfigOptions));
        
        
        
    }

    public IDatabase Database => _lazyConnection.Value.GetDatabase();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            if (_lazyConnection.IsValueCreated)
            {
                _lazyConnection.Value.Dispose();
            }
        }

        disposed = true;
    }
}
