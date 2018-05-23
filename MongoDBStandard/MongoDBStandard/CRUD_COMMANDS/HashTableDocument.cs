using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.CRUD_COMMANDS
{
    public class HashTableDocument
    {
        public ObjectId Id { get; set; }
        //[BsonExtraElements]
        public Dictionary<string, models.Permission> Values { get; set; }

    }
}
