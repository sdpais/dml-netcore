using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DML.Domain.MongoEntities;

public class RoleActionPermission
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    //    [BsonElement("EntityName")]
    //   public required string EntityName { get; set; }
    //[BsonRequired]
    public string EntityId { get; set; }
    //[BsonRequired]
    public string ClientId { get; set; }
    //[BsonRequired] 
    public string ModuleId { get; set; }
    //[BsonRequired]
    public string RoleId { get; set; }

    public bool CanEdit { get; set; }
    public bool CanView { get; set; }
    public bool CanDelete { get; set; }

    //[BsonIgnore]
    //public DateTime CreatedOn { get; set; }
    
    //[BsonIgnore]
    //public DateTime UpdatedOn { get; set; }
}
