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

            //Dictionary<BsonDocument, BsonDocument> users = new Dictionary<BsonDocument, BsonDocument>();


            //BsonDocument bsonDoc = users.ToBsonDocument();
            //byte[] content = new byte[10];


            //BsonDocument file = new BsonDocument
            //{
            //    {"Users", bsonDoc},
            //    {"GUID", "23"},
            //    {"Content", content.ToBsonDocumentArray()},
            //    {"Extension", ".txt"},
            //    {"Name", "File1"}
            //};

            //MongoCommands.UploadFile(file);


            List<BsonDocument> listOfGoals = new List<BsonDocument>();
            List<BsonDocument> listOfFiles = new List<BsonDocument>();
            List<BsonDocument> listOfNotes = new List<BsonDocument>();
            string username = "George78";
            string phoneNumber = "888-888-888";
            string Email = "George@gmail.com";

            BsonDocument user = new BsonDocument
            {
                {"ListOfGoals", listOfGoals.ToBsonDocumentArray()},
                {"ListOfNotes", listOfNotes.ToBsonDocumentArray()},
                {"ListOfFiles", listOfFiles.ToBsonDocumentArray()},
                {"Username", username},
                {"PhoneNumber", phoneNumber},
                {"Email", Email}
            };


            MongoCommands.CreateUser(user);


            string taskName = "Set MongoDB Database on Azure.";
            string description = "Azure is a cloud service provider.";
            string deadline = "5/19/2018";
            int points = 200;
            bool completed = false;
            bool hidden = false;
         
            BsonDocument goal = new BsonDocument

            {
                {"TaskName", taskName},
                {"Description", description},
                {"Deadline", deadline},
                {"Points", points},
                {"Completed", completed},
                {"Hidden", hidden}
            };
          


            MongoCommands.CreateGoal(goal, "natasha78");
      
        }
        
    }

}