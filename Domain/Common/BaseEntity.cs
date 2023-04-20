using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        [BsonId]
        public string ItemId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
