using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDBStandard.models;
using MongoDB.Bson.Serialization;

namespace MongoDBStandard.CRUD_COMMANDS
{
    public class MongoCommands
    {
        const string USER_COLLECTION = "UserAccount";
        const string NOTE_COLLECTION = "Note";
        const string FILE_COLLECTION = "File";

        const string MONGO_CONNECTION_STRING = "mongodb://40.114.29.68:27017";
        const string MONGO_DATABASE = "Mongo_Study_App";

        public void DeleteFile(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);
            fileCollection.DeleteOne(deleteFileFilter);

        }
        public void DeleteNote(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteNoteFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);
            fileCollection.DeleteOne(deleteNoteFilter);
        }
        public void CreateNote(Note note)
        {
            BsonDocument bNote = note.ToBsonDocument();
            IMongoCollection<BsonDocument> noteCollection = GetCollection(NOTE_COLLECTION);

            noteCollection.InsertOne(bNote);
        }
        public void UploadFile(File file)
        {
            BsonDocument bFile = file.ToBsonDocument();
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);

            fileCollection.InsertOne(bFile);
        }

        public void CreateUser(UserAccount user)
        {
            BsonDocument bUserAccount = user.ToBsonDocument();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);

            userCollection.InsertOne(bUserAccount);
        }

        public void CreateGoal(Goal goal, string username)

        {
            
            UserAccount account = GetUser(username);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();

            
            if((user["ListOfGoals"].AsBsonValue).BsonType == BsonType.Null)
            {
                Console.WriteLine("List is empty"); 
            } else
            {
                var userGoalsList = user["ListOfGoals"].AsBsonArray;
                if (userGoalsList != null)
                {
                    Console.WriteLine("Goals List is not null");
                    List<Goal> listOfGoals = new List<Goal>();


                    foreach (var element in userGoalsList)
                    {
                        Goal g = null;
                        var type = element["_t"].AsString;
                        if (type == "NonRecurringGoal")
                        {
                            g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                        }
                        else if (type == "RecurringGoal")
                        {
                            g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                        }
                        listOfGoals.Add(g);
                    }
                    listOfGoals.Add(goal);



                    UserAccount updatedAccount = GetUser(username);
                    userCollection.DeleteOne(updatedAccount.ToBsonDocument());
                    updatedAccount.ListOfGoals = listOfGoals;
                    userCollection.InsertOne(updatedAccount.ToBsonDocument());
                }
                else
                {
                    Console.WriteLine("Goals List is null.");
                }
            }
            
           
            
            //BsonDocument bgoal = listOfGoals.ToBsonDocument();
            //var dataArray = new BsonArray { };

            //foreach(Goal eachGoal in listOfGoals)
            //{
            //    dataArray.Add();
            //}
            //BsonArray dataFields = new BsonArray { bgoal };

            //UpdateDefinition<BsonDocument> update = new BsonDocument("$set", new BsonDocument { { "ListOfGoals", dataFields } });
           // userCollection.UpdateOne(getUserFilter, update, new UpdateOptions { IsUpsert = false });

        }

        public void MarkGoalAsComplete(string goalGuid, string userName)
        {
            Console.WriteLine("Username: " + userName);
            Console.WriteLine("Goal GUID: " + goalGuid);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("Username", userName);


            //get listOFGoalsArrray from Database

            FilterDefinition<BsonDocument> getGoalFilter = Builders<BsonDocument>.Filter.Eq("GUID", 24);
            var userGoal = userCollection.Find(getGoalFilter).First();
            Console.WriteLine("Updating User Goal: " + userGoal);

            //if null print goals list is empty
            //if goals list is not empty, print each goal with \n

            //then add the previous goal to dataFields and add goal after
            throw new NotImplementedException();
        }


        public void UpdateFile(File file, string guid)
        {
            //replacing the file at the file location
        }
        public static bool AuthenticateUser(string Username, string Password)
        {
            //login

            throw new NotImplementedException();
        }

        public static void ShareFile(string guid, Dictionary<BsonDocument, BsonDocument> Sharers)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUser(string userName)
        {
            Console.WriteLine("Started method..." + " " + userName);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", userName);
            BsonDocument user = userCollection.Find(getUserFilter).First();
            BsonType typeOfGoal = user["ListOfGoals"].AsBsonValue.BsonType;

            if(typeOfGoal == BsonType.Null)
            {
                Console.WriteLine("Goal is empty");
                var userFilesList = user["ListOfFiles"].AsBsonValue;
                var userNotesList = user["ListOfNotes"].AsBsonValue;
                var username = user["UserName"].AsBsonValue;
                var phoneNumber = user["PhoneNumber"].AsBsonValue;
                var email = user["Email"].AsBsonValue;

                List<File> listOfFiles = BsonSerializer.Deserialize<List<File>>(userFilesList.ToJson());
                List<Note> listOfNotes = BsonSerializer.Deserialize<List<Note>>(userNotesList.ToJson());
                string usernameStr = BsonSerializer.Deserialize<string>(username.ToJson());
                string phoneNumberStr = BsonSerializer.Deserialize<string>(phoneNumber.ToJson());
                string emailStr = BsonSerializer.Deserialize<string>(email.ToJson());

                UserAccount userAccount = new UserAccount(null, listOfNotes, listOfFiles, usernameStr, phoneNumberStr, emailStr);

                return userAccount;
            } else
            {
                var userGoalsList = user["ListOfGoals"].AsBsonArray;
                var userFilesList = user["ListOfFiles"].AsBsonValue;
                var userNotesList = user["ListOfNotes"].AsBsonValue;
                var username = user["UserName"].AsBsonValue;
                var phoneNumber = user["PhoneNumber"].AsBsonValue;
                var email = user["Email"].AsBsonValue;

                List<Goal> listOfGoals = new List<Goal>();
                foreach (var element in userGoalsList)
                {
                    Goal g = null;
                    var type = element["_t"].AsString;
                    if (type == "NonRecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                    }
                    else if (type == "RecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                    }
                    listOfGoals.Add(g);
                }

                List<File> listOfFiles = BsonSerializer.Deserialize<List<File>>(userFilesList.ToJson());
                List<Note> listOfNotes = BsonSerializer.Deserialize<List<Note>>(userNotesList.ToJson());
                string usernameStr = BsonSerializer.Deserialize<string>(username.ToJson());
                string phoneNumberStr = BsonSerializer.Deserialize<string>(phoneNumber.ToJson());
                string emailStr = BsonSerializer.Deserialize<string>(email.ToJson());

                UserAccount userAccount = new UserAccount(listOfGoals, listOfNotes, listOfFiles, usernameStr, phoneNumberStr, emailStr);
                return userAccount;
            }
            

           
        }


        public IMongoCollection<BsonDocument> GetUpcomingGoals(string username)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<BsonDocument> GetFilePreviews(string username)
        {
            throw new NotImplementedException();
        }

        private IMongoCollection<BsonDocument> GetCollection(string name)
        {
            MongoUrl url = new MongoUrl(MONGO_CONNECTION_STRING);
            MongoClient client = new MongoClient(url);
            IMongoDatabase db = client.GetDatabase(MONGO_DATABASE);
            return db.GetCollection<BsonDocument>(name);

        }
    }
}



