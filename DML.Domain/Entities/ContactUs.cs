using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DML.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class ContactUs
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //[BsonIgnore]
        //public string _id { get; }

        [BsonElement("internalid")]
        public string InternalId { get; set; }
        
        [BsonRequired]
        [BsonElement("fullname")]
        [BsonDefaultValue("")]
        public required string FullName { get; set; }
        
        [BsonRequired]
        [BsonElement("email")]
        [BsonDefaultValue("")]
        public required string Email { get; set; }
        
        [BsonElement("phone")]
        [BsonDefaultValue("")]
        public required string Phone{ get; set; }
        
        [BsonRequired]
        [BsonElement("message")]
        [BsonDefaultValue("")]
        public required string Message { get; set; }

        [BsonElement("spam")]
        [BsonDefaultValue(false)]
        public bool Spam { get; set; }
        
        [BsonDateTimeOptions]
        [BsonElement("createdon")]
        [BsonDefaultValue("1900-01-01T01:00:00.001Z")]
        public DateTime CreatedOn { get; set; }
    }
}
