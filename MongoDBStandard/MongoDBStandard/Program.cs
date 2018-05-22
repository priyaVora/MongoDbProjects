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
using MongoDBStandard.models;

namespace MongodbRND
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCommands cmd = new MongoCommands();
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
            string username = "prvora89";
            string phoneNumber = "888-888-888";
            string email = "prvora89@gmail.com";

            BsonDocument user = new BsonDocument

            {
                {"ListOfGoals", listOfGoals.ToBsonDocumentArray()},
                {"ListOfNotes", listOfNotes.ToBsonDocumentArray()},
                {"ListOfFiles", listOfFiles.ToBsonDocumentArray()},
                {"Username", username},
                {"PhoneNumber", phoneNumber},
                {"Email", email}
            };



            string guid = "24";
            string taskName = "Set MongoDB Database on Azure.";
            string description = "Azure is a cloud service provider.";
            string deadline = "5/19/2018";
            int points = 200;
            bool completed = false;
            bool hidden = false;

            BsonDocument bgoal = new BsonDocument

            {
                {"GUID", guid},
                {"TaskName", taskName},
                {"Description", description},
                {"Deadline", deadline},
                {"Points", points},
                {"Completed", completed},
                {"Hidden", hidden}
            };
            BsonDocument bgoal2 = new BsonDocument

            {
                {"GUID", "25"},
                {"TaskName", "Finish Database - Application connection."},
                {"Description", "Mongo Db is the database we are using."},
                {"Deadline", "5/32/12"},
                {"Points", "100"},
                {"Completed", false},
                {"Hidden", false}
            };


            NonRecurringGoal goal = new NonRecurringGoal();
            goal.GUID = "24";
            goal.TaskName = taskName;
            goal.Description = description;
            goal.Deadline = new DateTime();
            goal.Points = points;
            goal.Completed = completed;
            goal.Hidden = false;

            RecurringGoal goal2 = new RecurringGoal();
            goal2.GUID = "25";
            goal2.TaskName = "second task";
            goal2.Description = "second description";
            goal2.Deadline = new DateTime();
            goal2.Points = 100;
            goal2.Completed = false;
            goal2.Hidden = false;

            NonRecurringGoal addingGoal = new NonRecurringGoal();
            addingGoal.GUID = "1";
            addingGoal.TaskName = "added task";
            addingGoal.Description = "added description";
            addingGoal.Deadline = new DateTime();
            addingGoal.Points = 400;
            addingGoal.Completed = false;
            addingGoal.Hidden = false;

            List<Goal> goalsList = new List<Goal>();
            goalsList.Add(goal);
            goalsList.Add(goal2);
            //goalsList.Add(addingGoal);

            UserAccount userAccount = new UserAccount(null, null, null, username, phoneNumber, email);

            cmd.GetUser("prvora89");
            //cmd.CreateUser(userAccount);
            //cmd.CreateGoal(addingGoal, "prvora89");
            // cmd.MarkGoalAsComplete(25, userAccount.UserName);
            //MongoCommands.GetUser(username);
        }

    }

}