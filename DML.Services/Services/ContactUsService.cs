using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DML.Domain.Entities;
using DML.ConfigServices;
using DML.MongoDb.Services;
using MongoDB.Bson;

namespace DML.Services
{
    public class ContactUsService
    {
        private readonly ContactUsMongoService _contactUsMongoService;
        public ContactUsService(IConfiguration configuration) {
            _contactUsMongoService = new ContactUsMongoService(configuration);
        }
        public async Task<bool> CreateContactUs(ContactUs contactUs)
        {
            await _contactUsMongoService.Create(contactUs);
            return true;
        }
        public async Task<bool> UpdateContactUs(ContactUs contactUs)
        {
            await _contactUsMongoService.Update(contactUs);
            return true;
        }
        public async Task<bool> DeleteContactUs(string id)
        {
            await _contactUsMongoService.Delete(id);
            return true;
        }
        public async Task<List<ContactUs>> GetContactUs()
        {
            return await _contactUsMongoService.Get();
        }
        public async Task<ContactUs> GetContactUs(string id)
        {
            return await _contactUsMongoService.Get(id);
        }
    }
}
