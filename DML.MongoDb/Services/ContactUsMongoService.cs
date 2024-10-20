using MongoDB.Driver;
using DML.ConfigServices.Interfaces;
using DML.Domain.Entities;
using Microsoft.Extensions.Configuration;
using DML.ConfigServices.Services;

namespace DML.MongoDb.Services
{
    public class ContactUsMongoService
    {
        protected IMongoDbSettings _mongoDbSettings;
        private readonly IMongoDbSettings _db;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ContactUs> _contactUsCollection;
        public ContactUsMongoService(IConfiguration configuration)
        { 
            _mongoDbSettings = new MongoDbSettings();
            
            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            
            var _mongoDatabase = _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
            _contactUsCollection = _mongoDatabase.GetCollection<ContactUs>(_mongoDbSettings.CollectionName);

        }
        public async Task<ContactUs> Create(ContactUs contactUs)
        {
            if (contactUs != null) {
                contactUs.InternalId = Guid.NewGuid().ToString(); 
                contactUs.Spam = false;
                contactUs.CreatedOn = System.DateTime.Now;
                await _contactUsCollection.InsertOneAsync(contactUs);
            }
            return contactUs;
        }

        public async Task<List<ContactUs>> Get()
        {
            return await _contactUsCollection.Find(c => true).ToListAsync();
        }
        public async Task<ContactUs> Get(string id)
        {
            return await _contactUsCollection.Find(item => item.InternalId == id).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(ContactUs contactUs)
        {
            if (contactUs != null)
            {
                string id = contactUs.InternalId;
                
                await _contactUsCollection.ReplaceOneAsync(item => item.InternalId == id, contactUs);
            }
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            await _contactUsCollection.DeleteOneAsync(item => item.InternalId == id);
            return true;
        }
    }
}
