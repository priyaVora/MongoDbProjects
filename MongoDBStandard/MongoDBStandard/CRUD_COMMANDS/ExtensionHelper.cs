using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.CRUD_COMMANDS
{
    public static class ExtensionHelper
    {
        public static BsonArray ToBsonDocumentArray(this IEnumerable list)
        {
            var array = new BsonArray();
            foreach (var item in list)
            {
                array.Add(item.ToBson());
            }
            return array;
        }
    }
}
