namespace Test.WebAPIWDapper.Services
{
    public interface IRedisCacheHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string?> StringGetAsync(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task StringDeleteAsync(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> StringExistsAsync(string key);

    }
}
