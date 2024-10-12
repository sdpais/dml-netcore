using StackExchange.Redis;

namespace DML.RedisCache.Interfaces;
public interface IRedisDbProvider : IDisposable
{
    public IDatabase Database { get; }
}

