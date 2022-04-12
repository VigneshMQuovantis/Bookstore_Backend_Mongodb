using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.UserModels
{
    public class UserEntities
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }
        public string FullName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }
    }
}
