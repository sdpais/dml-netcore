namespace Test.WebAPIWDapper.Constants
{
    /// <summary>
    /// An attempt to build out a best practice with naming conventions for Redis cache keys
    /// Using https://medium.com/nerd-for-tech/unveiling-the-art-of-redis-key-naming-best-practices-6e20f3839e4a
    /// as a guide
    /// </summary>
    public class RedisCacheKeys
    {
        //add a constant string for the prefix of the key
        public const string EncryptionKey = "EncryptionKey";

    }
}
