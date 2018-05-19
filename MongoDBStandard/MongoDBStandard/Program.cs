using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;
using MongoDBStandard;
using MongoDBStandard.CRUD_COMMANDS;

namespace MongodbRND
{
    class Program
    {
        static void Main(string[] args)
        {
            Commands cmd = new Commands("Mongo_Study_App");
            cmd.CreateCollection("Files");
            cmd.GetListOfCollectionNames("Mongo_Study_App");
            BsonDocument nested = new BsonDocument {
                  { "name", "Annie Johns" },
                  { "fields", "Many Fields" },
                  { "address", new BsonDocument {
                  { "street", "143 Main St." },
                  { "city", "Salt Lake City" },
                  { "state", "SLC" },
                  { "zip", 94117}
         }
     }
 };
            cmd.AddDocumentToCollection(nested, "Files");
        }
    }

}