using StackExchange.Redis;

namespace WebAPIWDapper.Services
{
    public interface IRedisDbProvider : IDisposable
    {
        public IDatabase Database { get; }
    }
}
