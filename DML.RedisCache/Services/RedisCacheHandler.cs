using StackExchange.Redis;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using Microsoft.Extensions.Configuration;
using DML.RedisCache.Interfaces;
//source
//https://www.c-sharpcorner.com/article/jwt-token-authentication-using-the-net-core-6-web-api/
namespace DML.RedisCache.Services;

public class RedisCacheHandler : IRedisCacheHandler
{
    private IRedisDbProvider _redisDbProvider;

    public RedisCacheHandler()
    {

        _redisDbProvider = new RedisDbProvider();

        if (_redisDbProvider.Database == null)
        {
            throw new ArgumentNullException("The provided redisDbProvider or its database is null");
        }
    }

    private bool disposedValue;

    public Task StringDeleteAsync(string key)
    {
        var _ = _redisDbProvider.Database.StringGetDeleteAsync(key).ConfigureAwait(false);
        return Task.CompletedTask;
    }

    public async Task<bool> StringExistsAsync(string key)
    {
        return await _redisDbProvider.Database.KeyExistsAsync(key).ConfigureAwait(false);
    }

    public async Task<string?> StringGetAsync(string key)
    {
        return await _redisDbProvider.Database.StringGetAsync(key).ConfigureAwait(false);
    }

    public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null)
    {
        return await _redisDbProvider.Database.StringSetAsync(key, value, expiry).ConfigureAwait(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _redisDbProvider.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~RedisCacheHandler()
    {
        Dispose(disposing: false);
    }
}
