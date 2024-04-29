using StackExchange.Redis;

namespace Test.WebAPIWDapper.Services
{
    public interface IRedisDbProvider : IDisposable
    {
        public IDatabase Database { get; }
    }
}
